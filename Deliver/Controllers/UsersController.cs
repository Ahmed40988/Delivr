using Deliver.BLL.DTOs.Customer;
using Deliver.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController(IUserService userService,IAuthService authService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IAuthService _authService = authService;

        [HttpPost("Complete-Customer-Profile")]
        public async Task<IActionResult> CompleteCustomerProfile(CompleteCustomerDTO request)
        {
            var userid = User.GetUserId();
            var result = await _userService.CompleteCustomerprofileAsync(userid, request);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);

        }

        [HttpPost("Get-UserType")]
        public async Task<IActionResult> GetUserType([FromQuery] UserType userType)
        {
            var userid = User.GetUserId();
            var result = await _authService.GetUserType(userid, userType);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);

        }

    }
}
