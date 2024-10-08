﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.DTOs
{
    public record RoomToCreate
    {
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
        public List<string> RoomFacilities { get; set; }
        public bool IsRented { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
