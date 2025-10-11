namespace Deliver.BLL.DTOs.Category.SubCategory
{
    public record SubCategoryRequest
    (
        string Name,
        string Description,
        IFormFile? Icon,
        int ParentCategoryId
    );
}
