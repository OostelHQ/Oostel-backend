using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.Hostel.Entities
{
    public class HostelFacilities
    {
        public string Id { get; set; }
        public string FacilityName { get; set; }

        public string HostelId { get; set; }
        public virtual Hostel Hostel { get; set; }
    }
}
