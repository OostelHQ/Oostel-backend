using Oostel.Application.Modules.UserWallet.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserRolesProfiles.DTOs
{
    public class GetAllLandlordProfileDetails
    {
        public UserWalletBalanceDTO UserWalletBalanceDTO { get; set; }
        public UserDto UserDto { get; set; }
        public LandlordProfile LandlordProfile { get; set; }
    }

    public class LandlordProfile
    {
        public string? FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string StateOfOrigin { get; set; }
        public string Country { get; set; }
        public string? PictureUrl { get; set; }
        public string? Street { get; set; }
        public int ProfileViewCount { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Religion { get; set; }
        public int Age { get; set; }
    }

}
