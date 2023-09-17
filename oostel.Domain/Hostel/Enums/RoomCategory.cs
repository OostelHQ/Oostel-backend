using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.Hostel.Enums
{
    public enum RoomCategory
    {
        [Description("SingleRoom")]
        SingleRoom = 1,
        [Description("Selfcon")]
        Selfcon = 2,
        [Description("RoomAndParlour")]
        TwoBedRoomFat = 3,
        [Description("ThreeBedRoomFat")]
        ThreeBedRoomFat = 4,
        [Description("FaceToFace")]
        FaceToFace = 5
      
    }
}
