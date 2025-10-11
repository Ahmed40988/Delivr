using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Types;

namespace Deliver.BLL.DTOs.Customer
{
    public record CompleteCustomerDTO
        (
        string FirstName,
        string LastName,
        string  PhoneNumber,
       AddressDTo Address

        );
}
