using Deliver.Entities.Unit;
using System;
using System.Collections.Generic;

namespace Deliver.Entities.Entities
{
    public class City: TrackingBase
    {
        public int Id { get; set; }
        public int GovernmentId { get; set; }
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;

        public Government Government { get; set; } = default!;
        public ICollection<Zone> Zones { get; set; } = new List<Zone>();
    }

}
