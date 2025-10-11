using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.BLL.DTOs.Account.Validators
{
    public class AddressDToValidators : AbstractValidator<AddressDTo>
    {
        public AddressDToValidators()
        {
                RuleFor(x => x.Government)
                .NotEmpty()
                .NotNull();


                RuleFor(x => x.City)
                .NotEmpty()
                .NotNull();

                RuleFor(x => x.Zone)
                .NotEmpty()
                .NotNull();

                RuleFor(x => x.Street)
                .NotEmpty()
                .NotNull();
        }

    }
}
