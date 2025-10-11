using Deliver.BLL.DTOs.Category.SubCategory;
using Deliver.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubCategoryController(ISubCategoryServices services) : ControllerBase
{
    private readonly ISubCategoryServices _services = services;

    [HttpPost("")]
    public async Task<IActionResult> Create([FromForm] SubCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _services.CreateAsync(request);
        return result.IsSuccess ? Created() : result.ToProblem();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var result = await _services.GetByIdAsync(id);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var result = await _services.GetAllAsync();
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromForm] SubCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _services.UpdateAsync(id, request);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var result = await _services.DeleteAsync(id);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
