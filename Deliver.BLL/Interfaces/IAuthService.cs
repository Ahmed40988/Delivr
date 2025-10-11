using Deliver.BLL.DTOs.Email;
using Microsoft.AspNetCore.Identity.Data;
using ResendConfirmationEmailRequest = Deliver.BLL.DTOs.Email.ResendConfirmationEmailRequest;
using ResetPasswordRequest = Deliver.BLL.DTOs.Account.ResetPasswordRequest;
namespace Deliver.BLL.Interfaces;

public interface IAuthService
{
        Task<Result<TokenDTO>> LoginAsync(LoginDTO loginDto);
        Task<Result> GetUserType(int userid, UserType userType);     
        Task<Result<TokenDTO>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<Result> RegisterAsync(RegisterDTO registerDto);
        Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request);
        Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request);
        Task<Result> ResetPasswordAsync(ResetPasswordRequest request);
        Task<Result> VerifyResetOtpAsync(string email, string code);
        Task<Result> SendResetOtpAsync(string email);
}
