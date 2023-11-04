using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserRolesProfiles.DTOs
{
    public class GetAgentProfileDTO
    {
        public string UserId { get; set; }
        public string? FullName { get; set; }
        public string Email { get; set; }
        public DateTime JoinedDate { get; set; }
        public string PhoneNumber { get; set; }
        public string StateOfOrigin { get; set; }
        public int ProfileViewCount { get; set; }
        public string? PictureUrl { get; set; }
        public string Country { get; set; }
        //public int NumberOfHostelsCreated { get; set; }
       // public int NumberRoomsCreated { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Religion { get; set; }
        public string Age { get; set; }
    }
}
