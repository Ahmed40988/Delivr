using Deliver.BLL.DTOs.Category.ParentCategory;
namespace Deliver.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ParentCategoryController(IParentCategoryServices services) : ControllerBase
{
    private readonly IParentCategoryServices _services = services;
    [HttpPost("")]
    public async Task<IActionResult> Create([FromForm] ParentCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _services.CreateAsync(request, cancellationToken);
            return result.IsSuccess ? Created():result.ToProblem();
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var result = await _services.GetByIdAsync(id, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var result = await _services.GetAllAsync(cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ParentCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _services.UpdateAsync(id, request, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var result = await _services.DeleteAsync(id, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
