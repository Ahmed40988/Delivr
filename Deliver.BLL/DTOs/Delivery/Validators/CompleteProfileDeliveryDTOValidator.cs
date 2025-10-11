using Deliver.BLL.DTOs.Delivery;
using Deliver.Dal.Abstractions.Const;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.BLL.DTOs.Delivery.Validators
{
    public class CompleteProfileDeliveryDTOValidator : AbstractValidator<CompleteProfileDeliveryDTO>
    {
        public CompleteProfileDeliveryDTOValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
             .EmailAddress();


            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.LastName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Phone)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.city)
                .NotEmpty()
                .NotNull();


            RuleFor(x => x.Photo)
           .Must(file =>
           {
               if (file == null)
                   return true;
               var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
               var extension = Path.GetExtension(file.FileName).ToLower();
               return allowedExtensions.Contains(extension);
           })
           .WithMessage("Only .jpg, .jpeg, and .png file types are allowed.");

            RuleFor(x => x.Photo)
                .Must(file => file == null || file.Length <= 5 * 1024 * 1024)
                .WithMessage("Photo size must not exceed 5MB.");
        }
        }
    }
