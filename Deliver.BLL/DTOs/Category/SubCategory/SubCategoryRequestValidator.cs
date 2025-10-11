using FluentValidation;

namespace Deliver.BLL.DTOs.Category.SubCategory;

public class SubCategoryRequestValidator:AbstractValidator<SubCategoryRequest>
{
    public SubCategoryRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        RuleFor(x => x.ParentCategoryId).GreaterThan(0);
    }
}
