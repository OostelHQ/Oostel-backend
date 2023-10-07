using Oostel.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Common.Types.RequestFeatures
{
    public class HostelTypesParam : PagingParams
    {
        public HostelCategory? HostelCategory { get; set; }
        public string? PriceBudgetRange { get; set; }
    }
}
