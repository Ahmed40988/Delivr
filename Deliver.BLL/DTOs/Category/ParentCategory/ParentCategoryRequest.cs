namespace Deliver.BLL.DTOs.Category.ParentCategory;
public record ParentCategoryRequest
(
    string Name,
    string Description,
    IFormFile? Photo
);
