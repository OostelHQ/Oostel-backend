using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserProfiles.DTOs
{
    public class GetStudentProfileDTO
    {
        public string UserId { get; set; }
        public string? FullName { get; set; }
        public string Email { get; set; }
        public DateTime? JoinedDate { get; set; }
        public string StateOfOrigin { get; set; }
        public bool IsAvailable { get; set; }
        public string Area { get; set; }
        public decimal RoomBudgetAmount { get; set; }
        public string? PictureUrl { get; set; }
        public int ProfileViewCount { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public List<string> LikedStudentIds { get; set; }
        public List<string> StudentLikedIds { get; set; }
        public int SchoolLevel { get; set; }
        public string Religion { get; set; }
        public string Denomination { get; set; }
       // public int RentedHostels { get; set; }
        public string Age { get; set; }
        public string Hobby { get; set; }
    }
}
