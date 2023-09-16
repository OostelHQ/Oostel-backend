using Oostel.Application.Modules.UserProfiles.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserProfiles.Services
{
    public interface IUserProfilesService
    {
        Task<List<UserProfileDTO>> GetAllUserProfile();
        Task<UserProfileDTO> GetUserProfileById(string id);
        Task<bool> UpdateUserProfile(UserProfileDTO userProfileDTO);
        Task<bool> CreateUserProfile(UserProfileDTO userProfileDTO);

    }
}
