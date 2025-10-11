using Deliver.Entities.Unit;
using System;
using System.Collections.Generic;

namespace Deliver.Entities.Entities
{
    public class Zone: TrackingBase
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; } = default!;

        public City City { get; set; } = default!;
        public ICollection<Street> Streets { get; set; } = new List<Street>();
    }

}
