using Deliver.BLL.DTOs.Delivery;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Types;

namespace Deliver.BLL.DTOs.Customer.Validators
{
    public class CompleteCustomerDTOValidator : AbstractValidator<CompleteCustomerDTO>
    {
        public CompleteCustomerDTOValidator()
        {

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.LastName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull();

        }
    }
}
