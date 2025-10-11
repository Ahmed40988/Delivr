using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Deliver.BLL.DTOs.Category.ParentCategory
{
    public class ParentCategoryRequestValidation:AbstractValidator<ParentCategoryRequest>
    {
        public ParentCategoryRequestValidation()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must be less than 100 characters");
            RuleFor(x=>x.Description).NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description must be less than 500 characters");
            RuleFor(x=>x.Photo).NotNull().WithMessage("Photo is required");
        }
    }
}
