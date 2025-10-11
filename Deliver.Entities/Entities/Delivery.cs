using Deliver.Entities.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.Entities.Entities
{
    public class Delivery : TrackingBase
    {
        public int Id { get; set; }
        public decimal salary { get; set; }
        public decimal bonus { get; set; }
        public int rate { get; set; }
        public bool is_available { get; set; }
        public int vehicle_type_id { get; set; }
        public string city { get; set; }=string.Empty;
        public string PhotoUrl { get; set; }=string.Empty;
        public int ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public ICollection<VehicleType> Vehicles { get; set; } = [];
    }

}
