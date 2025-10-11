using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.BLL.DTOs.Category.ParentCategory
{
    public record ParentCategoryResponse
    (
        string Name,
        string Description,
        string ImageUrl
    );
    
}
