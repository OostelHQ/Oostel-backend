using Oostel.Domain.Hostel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.RequestFilters
{
    public class HostelTypesParam: PagingParams
    {
        public HostelCategory HostelCategory { get; set; }
    }
}
