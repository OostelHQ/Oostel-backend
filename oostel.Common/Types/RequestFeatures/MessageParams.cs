using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Common.Types.RequestFeatures
{
    public class MessageParams : PagingParams
    {
        public string Email { get; set; }
        public string Container { get; set; } = "Unread";
    }
}
