using Microsoft.AspNetCore.Http;
using Oostel.Application.Modules.UserProfiles.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserProfiles.Services
{
    public interface IUserRolesProfilesService
    {
        Task<List<GetStudentProfileDTO>> GetAllStudents();
        Task<List<GetStudentProfileDTO>> GetStudentById(string studentId);
        Task<bool> UpdateStudentProfile(StudentProfileDTO userProfileDTO);
        Task<List<GetLandlordProfileDTO>> GetLandlordsById(string studentId);
        Task<List<GetLandlordProfileDTO>> GetAllLandlords();
        Task<bool> CreateLandLordProfile(LandlordProfileDTO landlordProfileDTO);
        Task<bool> CreateStudentProfile(StudentProfileDTO userProfileDTO);
        Task<bool> UpdateLandLordProfile(LandlordProfileDTO updateLandlordProfileDTO);
        Task<bool> UploadDisplayPictureAsync(IFormFile file, string userId);
    }
}
