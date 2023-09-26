using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.UserAccessors
{
    public class UserAccessor : IUserAccessor
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager= userManager;
            _httpContextAccessor= httpContextAccessor;
        }

        public async Task<ApplicationUser> CheckIfTheUserExist(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public string GetUsername()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }
    }
}
