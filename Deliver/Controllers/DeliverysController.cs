using Deliver.BLL.DTOs.Delivery;
using Deliver.Dal.Abstractions;
using Deliver.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeliverysController(IDeliveryService deliveryService) : ControllerBase
    {
        private readonly IDeliveryService _deliveryService = deliveryService;

        [HttpPost("Choose-VehicleType")]
        public async Task<IActionResult> ChooseVehicleType([FromBody] VehicleTypeenum vehicle)
        {
            var userid = User.GetUserId();
            var result = await _deliveryService.ChooseVehicleTypeAsync(userid, vehicle);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpPost("Complete-Profile")]
        public async Task<IActionResult> CompleteProfile([FromForm] CompleteProfileDeliveryDTO request)
        {
            var userid = User.GetUserId();
            var result = await _deliveryService.CompleteDeliveryProfileasync(userid, request);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
    }
}
