using Deliver.BLL.DTOs.Account;
using Deliver.BLL.DTOs.Email;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
namespace Deliver.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginDto)
    {
        if (loginDto.ExtraData?.Any() == true)
        {
            return BadRequest(new
            {
                Message = "Unexpected properties found in request body.",
                ExtraFields = loginDto.ExtraData.Keys
            });
        }

        var response = await _authService.LoginAsync(loginDto);
        return response.IsSuccess ? Ok(response.Value) : response.ToProblem();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO registerDto)
    {
        if (registerDto.ExtraData?.Any() == true)
        {
            return BadRequest(new
            {
                Message = "Unexpected properties found in request body.",
                ExtraFields = registerDto.ExtraData.Keys
            });
        }

        var response = await _authService.RegisterAsync(registerDto);
        return response.IsSuccess ? Ok(response) : response.ToProblem();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var response = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
        return response.IsSuccess ? Ok(response.Value) : response.ToProblem();
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
    {
        var result = await _authService.ConfirmEmailAsync(request);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPost("resend-confirmation-email")]
    public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailRequest request)
    {
        var result = await _authService.ResendConfirmationEmailAsync(request);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPost("verify-reset-otp")]
    public async Task<IActionResult> VerifyResetOtp([FromBody] VerifyOtpRequest request)
    {
        var result = await _authService.VerifyResetOtpAsync(request.Email, request.Code);
        return result.IsSuccess ? Ok("OTP is valid.") : result.ToProblem();
    }

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request)
    {
        var result = await _authService.SendResetOtpAsync(request.Email);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var result = await _authService.ResetPasswordAsync(request);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [Route("account")]
    public class AccountController : Controller
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            var props = new AuthenticationProperties { RedirectUri = "/account/google-response" };
            return Challenge(props, GoogleDefaults.AuthenticationScheme);
        }

        [Authorize]
        [HttpGet("google-response")]
        public IActionResult GoogleResponse()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return Ok(claims);
        }
    }
}
