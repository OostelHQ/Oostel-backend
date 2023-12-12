using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserRolesProfiles.DTOs
{
    public class OpenToRoommateDTO
    {
        public string StudentId { get; set; }
        public bool GottenAHostel { get; set; }
        public decimal HostelPrice { get; set; }
        public string HostelAddress { get; set; }
    }
}
