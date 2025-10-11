using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.BLL.DTOs.Account
{
    public record AddressDTo
    (
        string Government,
        string City,
        string Zone,
        string Street
    );
}
