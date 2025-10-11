using Deliver.BLL.DTOs.Category.ParentCategory;

namespace Deliver.BLL.Interfaces;

public interface IParentCategoryServices
{
    Task<Result> CreateAsync(ParentCategoryRequest request,CancellationToken cancellationToken=default);
    Task<Result<bool>> UpdateAsync(int id, ParentCategoryRequest request,CancellationToken cancellationToken=default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<ParentCategoryResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<ParentCategoryResponse>>> GetAllAsync(CancellationToken cancellationToken =default);
}
