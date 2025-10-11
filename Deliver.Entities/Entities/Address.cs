using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.Entities.Entities
{

    public class Address
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; } = default!;
        public int StreetId { get; set; }
        public Street Street { get; set; } = default!;
    }
}
