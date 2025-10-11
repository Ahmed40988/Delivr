using Deliver.BLL.DTOs.Category.ParentCategory;
using Deliver.BLL.DTOs.Category.SubCategory;

namespace Deliver.BLL.Interfaces;

public interface ISubCategoryServices
{
    Task<Result>CreateAsync(SubCategoryRequest request);
    Task<Result<bool>>UpdateAsync(int id, SubCategoryRequest request);
    Task<Result<bool>>DeleteAsync(int id);
    Task<Result<SubCategoryResponse>>GetByIdAsync(int id);
    Task<Result<IEnumerable<SubCategoryResponse>>>GetAllAsync();
}
