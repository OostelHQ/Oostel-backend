using Microsoft.AspNetCore.Http;
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
        Task<List<GetUserProfileDTO>> GetAllUserProfile();
        Task<List<GetUserProfileDTO>> GetUserProfileById(string userId);
        Task<bool> UpdateUserProfile(UserProfileDTO userProfileDTO);
        Task<bool> CreateUserProfile(UserProfileDTO userProfileDTO);
        Task<bool> UploadDisplayPictureAsync(IFormFile file, string userId);
    }
}
