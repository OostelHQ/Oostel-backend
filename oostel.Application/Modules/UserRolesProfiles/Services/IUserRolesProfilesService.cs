using Microsoft.AspNetCore.Http;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Application.Modules.UserRolesProfiles.DTOs;
using Oostel.Common.Helpers;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.UserAuthentication.Entities;
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
        Task<GetAllStudentDetailsResponse> GetStudentById(string studentId);
        Task<bool> UpdateStudentProfile(UpdateStudentDTO userProfileDTO);
        Task<GetAllLandlordProfileDetails> GetLandlordsById(string landlordId);
        Task<bool> AvailableForRoommate(OpenToRoommateDTO openToRoommateDTO);
        Task<List<GetAgentProfileDTO>> GetAllAgents();
        Task<List<GetLandlordProfileDTO>> GetAllLandlords();
        Task<bool> CreateLandLordProfile(CreateLandlordDTO landlordProfileDTO);
        Task<bool> CreateStudentProfile(CreateStudentDTO userProfileDTO);
        Task<GetAllAgentProfileDetailsResponse> GetAgentById(string agentId);
        Task<bool> UpdateLandLordProfile(UpdateLandlordDTO updateLandlordProfileDTO);
        Task<bool> ProfileViewsCount(string userId);
        Task<BaseRoleResponse> GetCurrentUser();
        Task<bool> UploadDisplayPictureAsync(IFormFile file, string userId);
        Task<bool> AcceptLandlordInvitation(string agentId, string landlordId);
        Task<bool> AddStudentLike(string sourceId, string studentLikeId);
        Task<bool> UpdateAgentProfile(UpdateAgentProfileDTO updateAgentProfileDTO);
        Task<ApplicationUser> GetEmailAsync(string email);
        Task<bool> CreateAgentProfile(CreateAgentProfileDTO createAgentProfileDTO);
        Task<bool> CreateReferralCode(string landlordId);
        //Task<bool> SendAgentInvitationCode(string agentEmail, string referralCode, string landlordName, string shortNote);
        Task<string> GetLandlordReferralCode(string landlordId);
    }
}
