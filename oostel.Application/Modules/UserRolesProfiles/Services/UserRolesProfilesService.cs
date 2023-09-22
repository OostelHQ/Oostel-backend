using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Common.Enums;
using Oostel.Common.Helpers;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserRoleProfiles.Entities;
using Oostel.Infrastructure.Data;
using Oostel.Infrastructure.Media;
using Oostel.Infrastructure.Repositories;

namespace Oostel.Application.Modules.UserProfiles.Services
{
    public class UserRolesProfilesService : IUserRolesProfilesService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediaUpload _mediaUpload;
        private readonly ApplicationDbContext _applicationDbContext;
        public UserRolesProfilesService(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext, UnitOfWork unitOfWork, IMediaUpload mediaUpload, IMapper mapper)
        {
            _unitOfWork= unitOfWork;
            _userManager= userManager;
            _mapper= mapper;
            _mediaUpload = mediaUpload;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<GetStudentProfileDTO>> GetAllStudents()
        {
            var students = await _unitOfWork.StudentRepository.FindandInclude(x => x.User.RolesCSV.Contains(RoleType.Student.GetEnumDescription()), true);
            var studentsMapping = _mapper.Map<List<GetStudentProfileDTO>>(students);

            return studentsMapping;
        }

        public async Task<List<GetStudentProfileDTO>> GetStudentById(string studentId)
        {
            var student = await _unitOfWork.StudentRepository.FindandInclude(x => x.Id == studentId && x.User.RolesCSV.Contains(RoleType.Student.GetEnumDescription()), true);
            if (student is null) return null;

            var studentMapping = _mapper.Map<List<GetStudentProfileDTO>>(student);
            return studentMapping;
        }
        public async Task<bool> UpdateStudentProfile(StudentProfileDTO userProfileDTO)
        {
            var studentProfile =  _unitOfWork.StudentRepository.FindandInclude(x => x.Id == userProfileDTO.UserId && x.User.RolesCSV.Contains(RoleType.Student.GetEnumDescription()), true).Result.FirstOrDefault();
            if (studentProfile is null) return false;

            studentProfile.SchoolLevel = userProfileDTO.SchoolLevel ?? studentProfile.SchoolLevel;
            studentProfile.Age = userProfileDTO.Age ?? studentProfile.Age;
            studentProfile.Hobby = userProfileDTO.Hobby ?? studentProfile.Hobby;
            studentProfile.Gender = userProfileDTO.Gender ?? studentProfile.Gender ;
            studentProfile.Religion = userProfileDTO.Religion ?? studentProfile.Religion ;
            studentProfile.StateOfOrigin = userProfileDTO.StateOfOrigin ?? studentProfile.StateOfOrigin ;
            studentProfile.Denomination = userProfileDTO.Denomination ?? studentProfile.Denomination;
            studentProfile.PhoneNumber = userProfileDTO.PhoneNumber ?? studentProfile.PhoneNumber;
            studentProfile.User.FirstName = userProfileDTO.FirstName ?? studentProfile.User.FirstName;
            studentProfile.User.LastName = userProfileDTO.LastName ?? studentProfile.User.LastName;
            studentProfile.User.Email = userProfileDTO.Email ?? studentProfile.User.Email;
            studentProfile.LastModifiedDate = DateTime.UtcNow;

              await _unitOfWork.StudentRepository.UpdateAsync(studentProfile);
             var saveState = await _unitOfWork.SaveAsync() > 0;
             if(!saveState) return false;

            return true;
        }

        public async Task<bool> CreateStudentProfile(StudentProfileDTO updateStudentProfileDTO)
        {
            var studentProfile = _unitOfWork.StudentRepository.FindandInclude(x => x.Id == updateStudentProfileDTO.UserId && x.User.RolesCSV.Contains(RoleType.Student.GetEnumDescription()), true).Result.SingleOrDefault();
            if (studentProfile is null) return false;


            var student = new Student()
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
                User = new ApplicationUser
                {
                    FirstName = updateStudentProfileDTO.FirstName,
                    LastName = updateStudentProfileDTO.LastName,
                    Email = updateStudentProfileDTO.Email
                },
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
            };

            var checkIfUserProfileExist = await _unitOfWork.StudentRepository.Find(x => x.Id == updateStudentProfileDTO.UserId);
            if (checkIfUserProfileExist is null)
            {
                await _unitOfWork.StudentRepository.Add(student);
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
            var landlords = await _unitOfWork.StudentRepository.FindandInclude(x => x.User.RolesCSV.Contains(RoleType.LandLord.GetEnumDescription()), true);
            var landlordMapping = _mapper.Map<List<GetLandlordProfileDTO>>(landlords);

            return landlordMapping;
        }

        public async Task<List<GetLandlordProfileDTO>> GetLandlordsById(string landlordId)
        {
            var landlord = await _unitOfWork.LandlordRepository.FindandInclude(x => x.Id == landlordId && x.User.RolesCSV.Contains(RoleType.LandLord.GetEnumDescription()), true);
            if (landlord is null) return null;

            var landlordMapping = _mapper.Map<List<GetLandlordProfileDTO>>(landlord);
            return landlordMapping;
        }

        public async Task<bool> CreateLandLordProfile(LandlordProfileDTO landlordProfileDTO)
        {
            var landlordProfile = _unitOfWork.LandlordRepository.FindandInclude(x => x.Id == landlordProfileDTO.UserId && x.User.RolesCSV.Contains(RoleType.LandLord.GetEnumDescription()), true).Result.SingleOrDefault();
            if (landlordProfile is null) return false;


            var landlord = new Landlord()
            {
                Id = landlordProfile.Id,
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

            var checkIfLandlordExist = await _unitOfWork.LandlordRepository.Find(x => x.Id == landlordProfileDTO.UserId);
            if (checkIfLandlordExist is null)
            {
                await _unitOfWork.LandlordRepository.Add(landlord);
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
            var landlordProfile = _unitOfWork.LandlordRepository.FindandInclude(x => x.Id == landlordProfileDTO.UserId && x.User.RolesCSV.Contains(RoleType.LandLord.GetEnumDescription()), true).Result.SingleOrDefault();
            if (landlordProfile is null) return false;

            landlordProfile.Age = landlordProfileDTO.Age;
            landlordProfile.Religion = landlordProfileDTO.Religion ?? landlordProfile.Religion;
            landlordProfile.StateOfOrigin = landlordProfileDTO.StateOfOrigin ?? landlordProfile.StateOfOrigin;
            landlordProfile.DateOfBirth = landlordProfileDTO.DateOfBirth;
            landlordProfile.Country = landlordProfileDTO.Country ?? landlordProfile.Country;
            landlordProfile.PhoneNumber = landlordProfileDTO.PhoneNumber ?? landlordProfile.PhoneNumber;
            landlordProfile.User.FirstName = landlordProfileDTO.FirstName ?? landlordProfile.User.FirstName;
            landlordProfile.User.LastName = landlordProfileDTO.LastName ?? landlordProfile.User.LastName;
            landlordProfile.User.Email = landlordProfileDTO.Email ?? landlordProfile.User.Email;
            landlordProfile.LastModifiedDate = DateTime.UtcNow;

            await _unitOfWork.LandlordRepository.UpdateAsync(landlordProfile);
            var saveState = await _unitOfWork.SaveAsync() > 0;
            if (!saveState) return false;

            return true;
        }

        public async Task<bool> UploadDisplayPictureAsync(IFormFile file, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            if (user.RolesCSV.Contains(RoleType.LandLord.GetEnumDescription()))
            {
                var landlord = await _unitOfWork.LandlordRepository.GetById(userId);
                if (landlord is null) return false;

                var landlordPhotoUploadResult = await _mediaUpload.UploadPhoto(file);
                landlord.ProfilePhotoURL = landlordPhotoUploadResult.Url;

                await _unitOfWork.LandlordRepository.UpdateAsync(landlord);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                var student = await _unitOfWork.StudentRepository.GetById(userId);
                var studentPhotoUploadResult = await _mediaUpload.UploadPhoto(file);
                student.ProfilePhotoURL = studentPhotoUploadResult.Url;

                await _unitOfWork.StudentRepository.UpdateAsync(student);
                await _unitOfWork.SaveAsync();
            }

            return true;
        }

      
    }
}
