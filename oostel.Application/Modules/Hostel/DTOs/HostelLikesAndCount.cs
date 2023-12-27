using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.DTOs
{
    public class HostelLikesAndCount
    {
        public List<string> HostelLikedUsers { get; set; }
        public int HostelLikesCount { get; set; }
    }
}
