using Mailjet.Client.Resources;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Application.Modules.UserRolesProfiles.DTOs;
using Oostel.Application.UserAccessors;
using Oostel.Common.Enums;
using Oostel.Common.Helpers;
using Oostel.Domain.Hostel.Entities;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserRoleProfiles.Entities;
using Oostel.Domain.UserRolesProfiles.Entities;
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

        public async Task<bool> AvailableForRoommate(OpenToRoommateDTO openToRoommateDTO)
        {
            var student = await _unitOfWork.StudentRepository.FindandInclude(x => x.Id == openToRoommateDTO.StudentId,true);
            if (student is null) return false;

            var openToRoomate = OpenToRoommate.CreateOpenToRoomateFactory(openToRoommateDTO.StudentId, openToRoommateDTO.HostelName,
                                openToRoommateDTO.HostelPrice, openToRoommateDTO.HostelAddress);

            if (openToRoomate.Student.IsAvailable == true) 
                return false;

            openToRoomate.Student.IsAvailable = true;

            await _unitOfWork.OpenToRoommateRepository.Add(openToRoomate);
            await _unitOfWork.SaveAsync();

            return true;

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
            studentProfile.User.PhoneNumber = userProfileDTO.PhoneNumber ?? studentProfile.User.PhoneNumber;
            studentProfile.User.FirstName = userProfileDTO.FirstName ?? studentProfile.User.FirstName;
            studentProfile.User.LastName = userProfileDTO.LastName ?? studentProfile.User.LastName;
            studentProfile.User.Email = userProfileDTO.Email ?? studentProfile.User.Email;
            studentProfile.LastModifiedDate = DateTime.UtcNow;

              await _unitOfWork.StudentRepository.UpdateAsync(studentProfile);
             var saveState = await _unitOfWork.SaveAsync() > 0;
             if(!saveState) return false;

            return true;
        }

        public async Task<bool> CreateStudentProfile(CreateStudentDTO updateStudentProfileDTO)
        {
            var studentProfile = _userManager.Users.Any(x => x.Id == updateStudentProfileDTO.UserId && x.RolesCSV.Contains(RoleType.Student.GetEnumDescription()));
            if (!studentProfile) return false;


            var student = new Student()
            {
                Id = updateStudentProfileDTO.UserId,
                Age = updateStudentProfileDTO.Age,
                Gender = updateStudentProfileDTO.Gender,
                Hobby = updateStudentProfileDTO.Hobby,
                Religion = updateStudentProfileDTO.Religion,
                SchoolLevel = updateStudentProfileDTO.SchoolLevel,
                Denomination = updateStudentProfileDTO.Denomination,
                StateOfOrigin = updateStudentProfileDTO.StateOfOrigin,
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
            var landlords = await _unitOfWork.LandlordRepository.FindandInclude(x => x.User.RolesCSV.Contains(RoleType.LandLord.GetEnumDescription()), true);
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

        public async Task<bool> CreateLandLordProfile(CreateLandlordDTO landlordProfileDTO)
        {
            var user =  _userManager.Users.Any(x => x.Id == landlordProfileDTO.UserId && x.RolesCSV.Contains(RoleType.LandLord.GetEnumDescription()));
            if (!user) return false;

            var landlord = new Landlord()
            {
                Id = landlordProfileDTO.UserId,
                Age = landlordProfileDTO.Age,
                Religion = landlordProfileDTO.Religion,
                DateOfBirth = landlordProfileDTO.DateOfBirth,
                StateOfOrigin = landlordProfileDTO.StateOfOrigin,
                Country = landlordProfileDTO.Country,
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
            landlordProfile.User.PhoneNumber = landlordProfileDTO.PhoneNumber ?? landlordProfile.User.PhoneNumber;
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


                var userPictureResult = await _mediaUpload.UploadPhoto(file);
                user.ProfilePhotoURL = userPictureResult.Url;

                await _userManager.UpdateAsync(user);
            
                return true;
        }


        public async Task<bool> ProfileViewsCount(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            user.ProfileViewCount++;

            await _userManager.UpdateAsync(user);

            return true;
        }

        public async Task<bool> AddStudentLike(string sourceId, string studentLikeId)
        {
            var sourceUser = await _userManager.FindByIdAsync(sourceId);
            if (sourceUser is null) return false;

            var studentLiked = await _unitOfWork.HostelRepository.FindandInclude(x => x.Id == studentLikeId, true);
            if (studentLiked is null && studentLiked.Count() < 0) return false;

            if(sourceId.Equals(studentLikeId)) return false;

            var studentLike = await _unitOfWork.StudentLikesRepository.Find(x => x.SourceUserId == sourceId && x.LikedStudentId == studentLikeId);
            if (studentLike is null)
            {
                var likestudent = StudentLikes.CreateStudentLikesFactory(sourceId, studentLikeId);
                await _unitOfWork.StudentLikesRepository.Add(likestudent);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                await _unitOfWork.StudentLikesRepository.Remove(studentLike);
                await _unitOfWork.SaveAsync();
            }

            return true;
        }



    }
}
