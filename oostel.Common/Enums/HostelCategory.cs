using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Common.Enums
{
    public enum HostelCategory
    {
        [Description("Selfcon")]
        Selfcon = 1,
        [Description("One-Room")]
        OneRoom = 2,
        [Description("FaceToFace")]
        FaceToFace = 3,
        [Description("Flat")]
        Flat = 4,

    }
}
