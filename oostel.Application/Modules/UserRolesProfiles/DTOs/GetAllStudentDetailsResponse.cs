using Oostel.Application.Modules.UserWallet.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserRolesProfiles.DTOs
{
    public class GetAllStudentDetailsResponse : BaseRoleResponse
    {
        public UserWalletBalanceDTO UserWalletBalanceDTO { get; set; }
        public StudentProfile StudentProfile { get; set; }
    }

    public class UserDto 
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RolesCSV { get; set; }
        public DateTime? JoinedDate { get; set; }
        public DateTime? LastSeenDate { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
    }

    public class StudentProfile
    {
        public string? FullName { get; set; }
        public string Email { get; set; }
        public string StateOfOrigin { get; set; }
        public bool IsAvailable { get; set; }
        public string Area { get; set; }
        public decimal RoomBudgetAmount { get; set; }
        public string? PictureUrl { get; set; }
        public int ProfileViewCount { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string SchoolLevel { get; set; }
        public string Religion { get; set; }
        public string Denomination { get; set; }
        public string GuardianPhoneNumber { get; set; }
        // public int RentedHostels { get; set; }
        public string Age { get; set; }
        public string Hobby { get; set; }

    }


}
