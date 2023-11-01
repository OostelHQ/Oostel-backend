using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Common.Enums
{
    public enum RegisterRole
    {
        [Description("LandLord")]
        LandLord = 1,

        [Description("Student")]
        Student = 2,
    }
}
