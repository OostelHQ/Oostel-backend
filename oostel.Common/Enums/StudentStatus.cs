using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Common.Enums
{
    public enum StudentStatus
    {
        [Description("Gotten Hostel")]
        GottenHostel = 1,

        [Description("No Hostel")]
        NoHostel = 2
    }
}
