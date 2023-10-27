using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Common.Enums
{
    public enum PaymentStatus
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Completed")]
        Completed = 2,
        [Description("OnHold")]
        OnHold = 3,
        [Description("Failed")]
        Failed = 4,
    }
}
