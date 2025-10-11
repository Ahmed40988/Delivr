using System.Collections.Generic;

namespace Deliver.Entities.Entities;

public class SubCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty; 
    public int ParentCategoryId { get; set; }

    public virtual ParentCategory ParentCategory { get; set; } = default!;
    public virtual ICollection<Supplier> Suppliers { get; set; } =[];

}