﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserRolesProfiles.DTOs
{
    public class CreateStudentDTO
    {
        public string UserId { get; set; }
        public string? StateOfOrigin { get; set; }
        public string? Gender { get; set; }
        public string? SchoolLevel { get; set; }
        public string? Religion { get; set; }
        public string? Area { get; set; }
        public string? Denomination { get; set; }
        public string? Age { get; set; }
        public string? Hobby { get; set; }
    }
}
