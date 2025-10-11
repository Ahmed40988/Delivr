using Deliver.Entities.Unit;
using System;

namespace Deliver.Entities.Entities
{
    public class Street: TrackingBase
    {
        public int Id { get; set; }
        public int ZoneId { get; set; }
        public string Name { get; set; } = default!;
        public bool IsFavourite { get; set; }

        public Zone Zone { get; set; } = default!;
    }

}
