using Deliver.BLL.EmailSender;
using Deliver.BLL.Helper;
using Deliver.BLL.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
          .AddBusinessLogicConfig(configuration)
          .AddAuthConfig(configuration)
          .AddCors(configuration);

        var connectionString = configuration.GetConnectionString("default") ??
           throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return services;
    }

    private static IServiceCollection AddBusinessLogicConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IParentCategoryServices, ParentCategoryServices>();
      services.AddScoped<ISubCategoryServices, SubCategoryServices>();
        services.AddScoped<IDeliveryRepository, DeliveryRepository>();
        //services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<ISupplierServices, SupplierServices>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddMemoryCache();
        services.AddScoped<EmailBodyBuilder>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IDeliveryService, DeliveryService>();

        return services;
    }

    public static IServiceCollection AddEmailConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddTransient<IEmailSender>(provider =>
        {
            var emailSettings = provider.GetRequiredService<IOptions<MailSettings>>().Value;
            return new EmailSender(
                emailSettings.Email,
                emailSettings.AppPassword,
                emailSettings.Host,
                emailSettings.SSL,
                emailSettings.Port,
                emailSettings.IsBodyHtml
            );
        });
        return services;
    }

    private static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        services.AddOptions<JwtOptions>()
            .BindConfiguration("Jwt")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var jwtSettings = configuration.GetSection("Jwt").Get<JwtOptions>();

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                ValidIssuer = jwtSettings?.Issuer,
                ValidAudience = jwtSettings?.Audience
            };
        })
        .AddGoogle(options =>
        {
            var googleAuthSection = configuration.GetSection("Authentication:Google");
            options.ClientId = googleAuthSection["ClientId"]!;
            options.ClientSecret = googleAuthSection["ClientSecret"]!;
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        });

        return services;
    }

    private static IServiceCollection AddCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }
}
