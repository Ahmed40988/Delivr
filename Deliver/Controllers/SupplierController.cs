using Deliver.BLL.DTOs.Supplier;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplierController(ISupplierServices services) : ControllerBase
{
    private readonly ISupplierServices _services = services;
    [HttpPost(" ")]
    public async Task<IActionResult>CompleteProfleSupplier ([FromForm]SupplierRequest request)
    {
        var result = await _services.CreateSupplierAsync(request);
        return result.IsSuccess ? Created() : result.ToProblem();
    }
}
