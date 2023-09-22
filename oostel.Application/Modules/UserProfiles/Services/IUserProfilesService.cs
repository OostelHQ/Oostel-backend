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
        Task<List<GetStudentProfileDTO>> GetAllStudents();
        Task<List<GetStudentProfileDTO>> GetStudentById(string studentId);
        Task<bool> UpdateStudentProfile(UpdateStudentProfileDTO userProfileDTO);
        Task<bool> CreateStudentProfile(UpdateStudentProfileDTO userProfileDTO);
        Task<bool> UploadDisplayPictureAsync(IFormFile file, string userId);
    }
}
