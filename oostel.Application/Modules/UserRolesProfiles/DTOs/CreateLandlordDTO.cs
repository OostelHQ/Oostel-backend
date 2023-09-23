﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserRolesProfiles.DTOs
{
    public class CreateLandlordDTO
    {
        public string UserId { get; set; }
        public string StateOfOrigin { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Religion { get; set; }
        public int Age { get; set; }
    }
}
