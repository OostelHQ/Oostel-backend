﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.DTOs
{
    public class RoomPicturesDTO
    {
        public IFormFile PictureUrl { get; set; }
    }
}
