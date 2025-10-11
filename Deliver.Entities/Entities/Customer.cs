using Deliver.Entities.Unit;

namespace Deliver.Entities.Entities;
public class Customer: TrackingBase
{
    public int Id { get; set; }
    public string FirstLine { get; set; } = string.Empty;
    public int Points { get; set; } = 0;    
    public int ApplicationUserId { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }
}
