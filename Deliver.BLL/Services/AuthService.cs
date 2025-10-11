using System.Security.Cryptography;
using Deliver.BLL.DTOs.Email;
using Deliver.BLL.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using static System.Net.WebRequestMethods;
using ResendConfirmationEmailRequest = Deliver.BLL.DTOs.Email.ResendConfirmationEmailRequest;
using ResetPasswordRequest = Deliver.BLL.DTOs.Account.ResetPasswordRequest;

namespace Deliver.BLL.Services;
public class AuthService(
    IUserRepository userRepository
    , IUnitOfWork unitOf,
    SignInManager<ApplicationUser> signInManager,
    IJwtProvider jwtProvider,
     ILogger<AuthService> logger,
    UserManager<ApplicationUser> userManager
    , IMemoryCache cache,
    IEmailSender emailSender,
    EmailBodyBuilder builder) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOf = unitOf;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly ILogger<AuthService> _logger = logger;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IMemoryCache _cache = cache;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly EmailBodyBuilder builder = builder;
    private readonly int _refreshTokenExpiryDays = 14;
    private readonly int _otpExpiryMinutes = 5;


    public async Task<Result<TokenDTO>> LoginAsync(LoginDTO loginDto)
    {
        var user = await _userRepository.FindByEmailAsync(loginDto.Email);

        if (user == null)
            return Result.Failure<TokenDTO>(UserErrors.UserNotFound);

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded)
            return Result.Failure<TokenDTO>(UserErrors.InvalidCredentials);

        var roles = await _userRepository.GetRolesAsync(user);

        var tokenResult = _jwtProvider.GenerateToken(user);
        var newRefreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);
        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = refreshTokenExpiration
        });
        var tokenDto = new TokenDTO(
            user.Id,
            tokenResult.token,
            tokenResult.expiresIn,
            newRefreshToken,
            refreshTokenExpiration
        );
        return Result.Success(tokenDto);
    }
    public async Task<Result<TokenDTO>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
    {
        var user = _jwtProvider.ValidateToken(token);
        if (user is null)
            return Result.Failure<TokenDTO>(UserErrors.InvalidJwtToken);
        var userId = await _userManager.FindByIdAsync(user);
        if (userId is null)
            return Result.Failure<TokenDTO>(UserErrors.InvalidJwtToken);
        var userRefreshToken = userId.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);
        if (userRefreshToken is null)
            return Result.Failure<TokenDTO>(UserErrors.InvalidRefreshToken);

        userRefreshToken.RevokedOn = DateTime.UtcNow;

        var (newToken, expiresIn) = _jwtProvider.GenerateToken(userId);
        var newRefreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

        userId.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = refreshTokenExpiration
        });

        await _userManager.UpdateAsync(userId);

        var response = new TokenDTO(userId.Id,  newToken, expiresIn, newRefreshToken, refreshTokenExpiration);

        return Result.Success(response);

    }
    public async Task<Result> RegisterAsync(RegisterDTO registerDto)
    {
        var exist = await _userRepository.Any(registerDto.Email);

        if (exist == true)
            return Result.Failure<TokenDTO>(UserErrors.DuplicatedEmail);


        var user = new ApplicationUser
        {
            Email = registerDto.Email,
            UserName = registerDto.Email
        };
        var result = await _userRepository.CreateAsync(user, registerDto.Password);


        if (result.Succeeded)
        {

            // TODO: إضافة Role
            // var role = registerDto.UserType.ToString();
            // await _userRepository.AddToRoleAsync(user, role);

            await SendOtpAsync(user);

          
        }

        var error = result.Errors.FirstOrDefault();
        return Result.Failure(
            new Error(error.Code, error.Description, StatusCodes.Status409Conflict)
        );
    }
  

    public async Task<Result> ResendConfirmationEmailAsync(DTOs.Email.ResendConfirmationEmailRequest request)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
            return Result.Success();

        if (user.EmailConfirmed)
            return Result.Failure(UserErrors.DuplicatedConfirmation);

        await SendOtpAsync(user);

        return Result.Success();
    }

    public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
            return Result.Failure(UserErrors.InvalidCode);

        if (user.EmailConfirmed)
            return Result.Failure(UserErrors.DuplicatedConfirmation);

        if (!_cache.TryGetValue($"OTP_{user.Id}", out string? cachedOtp) || cachedOtp != request.Code)
            return Result.Failure(UserErrors.InvalidCode);

        _cache.Remove($"OTP_{user.Id}");

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (result.Succeeded)
        {
            return Result.Success();
        }

        var error = result.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }


    public async Task<Result> VerifyResetOtpAsync(string email, string code)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure(UserErrors.UserNotFound);

        var user = await _userRepository.FindByEmailAsync(email);
        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);

        if (!user.EmailConfirmed)
            return Result.Failure(UserErrors.EmailNotConfirmed);

        if (_cache.TryGetValue($"OTP_{user.Id}", out string? cachedOtp))
        {
            if (cachedOtp == code)
            {
                return Result.Success();
            }
        }

        return Result.Failure(UserErrors.InvalidCode);
    }

    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public async Task<Result> GetUserType(int userid,UserType userType)
    {
        await _userRepository.CreateUserProfileAsync(userid,userType);
        return Result.Success();
    }
        
    private async Task<Result> SendOtpAsync(ApplicationUser user)
    {
        var otpCode = new Random().Next(100000, 999999).ToString();

        _cache.Set($"OTP_{user.Id}", otpCode, TimeSpan.FromMinutes(_otpExpiryMinutes));

        var emailBody = builder.GenerateEmailBody("EmailConfirmation",
            new Dictionary<string, string>
            {
            { "{{name}}", user.FirstName },
            { "{{otp_code}}", otpCode },
            { "{{expiry_minutes}}", _otpExpiryMinutes.ToString() }
            }
        );

        await _emailSender.SendEmailAsync(user.Email!, "✅ Deliver: Email Verification OTP", emailBody);

        _logger.LogInformation("OTP cached and sent to user {UserId}: {OtpCode}", user.Id, otpCode);

        return Result.Success();
    }
    private async Task<Result> SendPasswordResetOtpAsync(ApplicationUser user)
    {

        var otpCode = new Random().Next(100000, 999999).ToString();

        _cache.Set($"OTP_{user.Id}", otpCode, TimeSpan.FromMinutes(_otpExpiryMinutes));

        

        var emailBody = builder.GenerateEmailBody("ForgetPassword",
            templateModel: new Dictionary<string, string>
            {
                { "{{name}}", user.FirstName },
                { "{{otp_code}}", otpCode },
                { "{{expiry_minutes}}", _otpExpiryMinutes.ToString() }
            }
        );

        await _emailSender.SendEmailAsync(user.Email!, "🔐 Deliver: Password Reset OTP", emailBody);

        _logger.LogInformation("Password reset OTP sent to user {UserId}: {OtpCode}", user.Id, otpCode);

        return Result.Success();
    }

    public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null || !user.EmailConfirmed)
            return Result.Failure(UserErrors.InvalidCode);

        if (!_cache.TryGetValue($"OTP_{user.Id}", out string? cachedOtp) || cachedOtp != request.Code)
            return Result.Failure(UserErrors.InvalidCode);

        _cache.Remove($"OTP_{user.Id}");

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, resetToken, request.newPassword);

        if (result.Succeeded)
        {
            return Result.Success();
        }
        var error = result.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }


    public async Task<Result> SendResetOtpAsync(string email)
    {
        if (await _userManager.FindByEmailAsync(email) is not { } user)
            return Result.Failure(UserErrors.UserNotFound);
        if (!user.EmailConfirmed)
            return Result.Failure(UserErrors.EmailNotConfirmed);
        await SendPasswordResetOtpAsync(user);
        return Result.Success();
    }
}
