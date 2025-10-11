using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.BLL.DTOs.Category.SubCategory
{
    public record SubCategoryResponse
   (
        string Name,
        string ?ImageUrl,
        int ParentCategoryId
   );
}
