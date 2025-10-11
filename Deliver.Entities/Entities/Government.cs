using Deliver.Entities.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.Entities.Entities
{
    public class Government: TrackingBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public ICollection<City> Cities { get; set; } = new List<City>();
    }

}
