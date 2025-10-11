using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.BLL.DTOs.Delivery
{
    public record CompleteProfileDeliveryDTO
    (
      string FirstName,
      string LastName,
      string Email,
      string Phone,
      string city,
    IFormFile? Photo
    );
}
