using Oostel.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Common.Types.RequestFeatures
{
    public class StudentTypeParams : PagingParams
    {
       // public bool? GottenHostel { get; set; }
        public string? Level { get; set; }
        public string? Gender { get; set; }
        public decimal? MinimumPrice { get; set; }
        public decimal? MaximumPrice { get; set; }
    }
}
