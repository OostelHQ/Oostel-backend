using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Common.Enums;
using Oostel.Common.Helpers;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserProfiles.Entities;
using Oostel.Infrastructure.Data;
using Oostel.Infrastructure.Media;
using Oostel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserProfiles.Services
{
    public class UserProfilesService : IUserProfilesService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediaUpload _mediaUpload;
        private readonly ApplicationDbContext _applicationDbContext;
        public UserProfilesService(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext, UnitOfWork unitOfWork, IMediaUpload mediaUpload, IMapper mapper)
        {
            _unitOfWork= unitOfWork;
            _userManager= userManager;
            _mapper= mapper;
            _mediaUpload = mediaUpload;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<GetStudentProfileDTO>> GetAllStudents()
        {
            var students = await _unitOfWork.UserProfileRepository.FindandInclude(x => x.User.RolesCSV.Contains(RoleType.Student.GetEnumDescription()), true);
            var studentsMapping = _mapper.Map<List<GetStudentProfileDTO>>(students);

            return studentsMapping;
        }

        public async Task<List<GetStudentProfileDTO>> GetStudentById(string studentId)
        {
            var student = await _unitOfWork.UserProfileRepository.FindandInclude(x => x.Id == studentId && x.User.RolesCSV.Contains(RoleType.Student.GetEnumDescription()), true);
            if (student is null) return null;

            var studentMapping = _mapper.Map<List<GetStudentProfileDTO>>(student);
            return studentMapping;
        }
        public async Task<bool> UpdateStudentProfile(StudentProfileDTO userProfileDTO)
        {
            var studentProfile =  _unitOfWork.UserProfileRepository.FindandInclude(x => x.Id == userProfileDTO.UserId, true).Result.SingleOrDefault();
            if (studentProfile is null) return false;

            studentProfile.SchoolLevel = userProfileDTO.SchoolLevel ?? studentProfile.SchoolLevel;
            studentProfile.Age = userProfileDTO.Age ?? studentProfile.Age;
            studentProfile.Hobby = userProfileDTO.Hobby ?? studentProfile.Hobby;
            studentProfile.Gender = userProfileDTO.Gender ?? studentProfile.Gender ;
            studentProfile.Religion = userProfileDTO.Religion ?? studentProfile.Religion ;
            studentProfile.StateOfOrigin = userProfileDTO.StateOfOrigin ?? studentProfile.StateOfOrigin ;
            studentProfile.Denomination = userProfileDTO.Denomination ?? studentProfile.Denomination;
            studentProfile.Country = userProfileDTO.Country ?? studentProfile.Country;
            studentProfile.PhoneNumber = userProfileDTO.PhoneNumber ?? studentProfile.PhoneNumber;
            studentProfile.User.FirstName = userProfileDTO.FirstName ?? studentProfile.User.FirstName;
            studentProfile.User.LastName = userProfileDTO.LastName ?? studentProfile.User.LastName;
            studentProfile.User.Email = userProfileDTO.Email ?? studentProfile.User.Email;
            studentProfile.LastModifiedDate = DateTime.UtcNow;

              await _unitOfWork.UserProfileRepository.UpdateAsync(studentProfile);
             var saveState = await _unitOfWork.SaveAsync() > 0;
             if(!saveState) return false;

            return true;
        }

        public async Task<bool> CreateStudentProfile(StudentProfileDTO updateStudentProfileDTO)
        {
            var studentProfile = _unitOfWork.UserProfileRepository.FindandInclude(x => x.Id == updateStudentProfileDTO.UserId, true).Result.SingleOrDefault();
            if (studentProfile is null) return false;


            var userProfile = new UserProfile()
            {
                Id = studentProfile.Id,
                Age = updateStudentProfileDTO.Age,
                Gender = updateStudentProfileDTO.Gender,
                Hobby = updateStudentProfileDTO.Hobby,
                Religion = updateStudentProfileDTO.Religion,
                SchoolLevel = updateStudentProfileDTO.SchoolLevel,
                Denomination = updateStudentProfileDTO.Denomination,
                PhoneNumber = updateStudentProfileDTO.PhoneNumber,
                StateOfOrigin = updateStudentProfileDTO.StateOfOrigin,
                Country = updateStudentProfileDTO.Country,
                User = new ApplicationUser
                {
                    FirstName = updateStudentProfileDTO.FirstName,
                    LastName = updateStudentProfileDTO.LastName,
                    Email = updateStudentProfileDTO.Email
                },
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
            };

            var checkIfUserProfileExist = await _unitOfWork.UserProfileRepository.Find(x => x.Id == updateStudentProfileDTO.UserId);
            if (checkIfUserProfileExist is null)
            {
                await _unitOfWork.UserProfileRepository.Add(userProfile);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                return false;
            }

            return true;
        }

        public async Task<List<GetLandlordProfileDTO>> GetAllLandlords()
        {
            var landlords = await _unitOfWork.UserProfileRepository.FindandInclude(x => x.User.RolesCSV.Contains(RoleType.LandLord.GetEnumDescription()), true);
            var landlordMapping = _mapper.Map<List<GetLandlordProfileDTO>>(landlords);

            return landlordMapping;
        }

        public async Task<List<GetLandlordProfileDTO>> GetLandlordsById(string landlordId)
        {
            var landlord = await _unitOfWork.UserProfileRepository.FindandInclude(x => x.Id == landlordId && x.User.RolesCSV.Contains(RoleType.LandLord.GetEnumDescription()), true);
            if (landlord is null) return null;

            var landlordMapping = _mapper.Map<List<GetLandlordProfileDTO>>(landlord);
            return landlordMapping;
        }

        public async Task<bool> CreateLandLordProfile(LandlordProfileDTO landlordProfileDTO)
        {
            var studentProfile = _unitOfWork.UserProfileRepository.FindandInclude(x => x.Id == landlordProfileDTO.UserId, true).Result.SingleOrDefault();
            if (studentProfile is null) return false;


            var userProfile = new UserProfile()
            {
                Id = studentProfile.Id,
                Age = landlordProfileDTO.Age,
                Religion = landlordProfileDTO.Religion,
                DateOfBirth = landlordProfileDTO.DateOfBirth,
                PhoneNumber = landlordProfileDTO.PhoneNumber,
                StateOfOrigin = landlordProfileDTO.StateOfOrigin,
                Country = landlordProfileDTO.Country,
                User = new ApplicationUser
                {
                    FirstName = landlordProfileDTO.FirstName,
                    LastName = landlordProfileDTO.LastName,
                    Email = landlordProfileDTO.Email
                },
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
            };

            var checkIfUserProfileExist = await _unitOfWork.UserProfileRepository.Find(x => x.Id == landlordProfileDTO.UserId);
            if (checkIfUserProfileExist is null)
            {
                await _unitOfWork.UserProfileRepository.Add(userProfile);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                return false;
            }

            return true;
        }
        public async Task<bool> UpdateLandLordProfile(LandlordProfileDTO landlordProfileDTO)
        {
            var landlordProfile = _unitOfWork.UserProfileRepository.FindandInclude(x => x.Id == landlordProfileDTO.UserId, true).Result.SingleOrDefault();
            if (landlordProfile is null) return false;

            landlordProfile.Age = landlordProfileDTO.Age ?? landlordProfile.Age;
            landlordProfile.Religion = landlordProfileDTO.Religion ?? landlordProfile.Religion;
            landlordProfile.StateOfOrigin = landlordProfileDTO.StateOfOrigin ?? landlordProfile.StateOfOrigin;
            landlordProfile.DateOfBirth = landlordProfileDTO.DateOfBirth;
            landlordProfile.Country = landlordProfileDTO.Country ?? landlordProfile.Country;
            landlordProfile.PhoneNumber = landlordProfileDTO.PhoneNumber ?? landlordProfile.PhoneNumber;
            landlordProfile.User.FirstName = landlordProfileDTO.FirstName ?? landlordProfile.User.FirstName;
            landlordProfile.User.LastName = landlordProfileDTO.LastName ?? landlordProfile.User.LastName;
            landlordProfile.User.Email = landlordProfileDTO.Email ?? landlordProfile.User.Email;
            landlordProfile.LastModifiedDate = DateTime.UtcNow;

            await _unitOfWork.UserProfileRepository.UpdateAsync(landlordProfile);
            var saveState = await _unitOfWork.SaveAsync() > 0;
            if (!saveState) return false;

            return true;
        }

        public async Task<bool> UploadDisplayPictureAsync(IFormFile file, string userId)
        {
            var user = await _unitOfWork.UserProfileRepository.GetById(userId);
            if (user == null) return false;

            var photoUploadResult = await _mediaUpload.UploadPhoto(file);
            user.ProfilePhotoURL = photoUploadResult.Url;

            await _unitOfWork.UserProfileRepository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();

            return true;
        }

      
    }
}
