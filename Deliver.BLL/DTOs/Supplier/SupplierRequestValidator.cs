using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Deliver.BLL.DTOs.Supplier;

public class SupplierRequestValidator:AbstractValidator<SupplierRequest>
{
    public SupplierRequestValidator()
    {
        RuleFor(x=> x.OwnerName)
            .NotEmpty().WithMessage("Owner name is required.")
            .MaximumLength(100).WithMessage("Owner name must not exceed 100 characters.");
        RuleFor(x => x.ShopName)
            .NotEmpty().WithMessage("Shop name is required.")
            .MaximumLength(100).WithMessage("Shop name must not exceed 100 characters.");
        RuleFor(x => x.ShopDescription)
            .MaximumLength(500).WithMessage("Shop description must not exceed 500 characters.");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number is not valid.");
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(200).WithMessage("Address must not exceed 200 characters.");
        RuleFor(x => x.OpeningTime)
            .NotNull().WithMessage("Opening time is required.");
        RuleFor(x => x.ClosingTime)
            .NotNull().WithMessage("Closing time is required.")
            .GreaterThan(x => x.OpeningTime).WithMessage("Closing time must be after opening time.");
    }
}
