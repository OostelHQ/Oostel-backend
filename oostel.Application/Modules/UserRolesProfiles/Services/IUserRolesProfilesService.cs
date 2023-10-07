using Microsoft.AspNetCore.Http;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Application.Modules.UserRolesProfiles.DTOs;
using Oostel.Common.Helpers;
using Oostel.Common.Types.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserProfiles.Services
{
    public interface IUserRolesProfilesService
    {
        Task<ResultResponse<PagedList<GetStudentProfileDTO>>> GetAllStudents(StudentTypeParams studentTypeParams);
        Task<List<GetStudentProfileDTO>> GetStudentById(string studentId);
        Task<bool> UpdateStudentProfile(StudentProfileDTO userProfileDTO);
        Task<List<GetLandlordProfileDTO>> GetLandlordsById(string studentId);
        Task<bool> AvailableForRoommate(OpenToRoommateDTO openToRoommateDTO);
        Task<List<GetLandlordProfileDTO>> GetAllLandlords();
        Task<bool> CreateLandLordProfile(CreateLandlordDTO landlordProfileDTO);
        Task<bool> CreateStudentProfile(CreateStudentDTO userProfileDTO);
        Task<bool> UpdateLandLordProfile(LandlordProfileDTO updateLandlordProfileDTO);
        Task<bool> ProfileViewsCount(string userId);
        Task<bool> UploadDisplayPictureAsync(IFormFile file, string userId);
        Task<bool> AddStudentLike(string sourceId, string studentLikeId);
    }
}
