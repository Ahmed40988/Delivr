using Deliver.Dal.Abstractions.Const;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.BLL.DTOs.Account.Validators
{
    public class RegisterDTOValidator:AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
             .EmailAddress();

            RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .MaximumLength(12).WithMessage("Password must not exceed 12 characters")
            .Matches(RegexPatterns.Password).WithMessage("Password must contain uppercase and lowercase letters, numbers, and special characters");


            //RuleFor(x => x.FirstName)
            //    .NotEmpty()
            //    .NotNull();

            //RuleFor(x => x.LastName)
            //    .NotEmpty()
            //    .NotNull();

            //RuleFor(x => x.Phone)
            //    .NotEmpty()
            //    .NotNull();

            //RuleFor(x => x.UserType)
            //.IsInEnum()
            //.WithMessage("UserType must be Customer, Delivery, or Supplier or 1,2,3.");



        }


    }
}
