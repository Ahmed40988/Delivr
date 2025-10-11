using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.BLL.DTOs.Account.Validators
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(x => x.Email)
              .NotEmpty()
              .NotNull()
              .EmailAddress();

            RuleFor(x => x.Password)
                 .NotNull()
                .NotEmpty();
        }

    }
}
