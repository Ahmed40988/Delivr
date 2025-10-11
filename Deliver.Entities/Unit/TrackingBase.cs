
using System;

namespace Deliver.Entities.Unit
{
    public class TrackingBase
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
