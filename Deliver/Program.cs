var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencies(builder.Configuration);

builder.Services
    .AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblies(new[]
    {
        Assembly.GetExecutingAssembly(),
        typeof(LoginDTO).Assembly,
        typeof(ApplicationDbContext).Assembly
    });

builder.Services.AddEmailConfig(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();   
app.UseAuthorization();
app.MapControllers();

app.Run();
