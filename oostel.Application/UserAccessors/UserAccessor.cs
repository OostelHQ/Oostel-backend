using Microsoft.AspNetCore.Identity;
using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.UserAccessors
{
    public class UserAccessor
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserAccessor(UserManager<ApplicationUser> userManager)
        {
            _userManager= userManager;
        }

        public async Task<ApplicationUser> CheckIfTheUserExist(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }
    }
}
