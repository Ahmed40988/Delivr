
using System.Collections.Generic;

namespace Deliver.Entities.Entities;
public class ParentCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string  Description { get; set; }= string.Empty;
    public string  Icon { get; set; } =string.Empty;
    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();

}
