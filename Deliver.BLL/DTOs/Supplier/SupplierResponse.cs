using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.BLL.DTOs.Supplier
{
    public record SupplierResponse
    (
        string Name,
        string ShoNtName,
        string Address,
        string Phone,
       TimeSpan Open,
         TimeSpan Close
    );
    
}
