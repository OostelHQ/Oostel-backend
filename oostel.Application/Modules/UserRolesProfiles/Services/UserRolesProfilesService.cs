using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.UserAuthentication.DTOs;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Application.Modules.UserRolesProfiles.DTOs;
using Oostel.Application.Modules.UserWallet.DTOs;
using Oostel.Application.RequestFilters;
using Oostel.Application.UserAccessors;
using Oostel.Common.Constants;
using Oostel.Common.Enums;
using Oostel.Common.Helpers;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.Hostel.Entities;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserRoleProfiles.Entities;
using Oostel.Domain.UserRolesProfiles.Entities;
using Oostel.Infrastructure.Data;
using Oostel.Infrastructure.EmailService;
using Oostel.Infrastructure.Media;
using Oostel.Infrastructure.Repositories;
using System.Net;
using System.Security.Claims;

namespace Oostel.Application.Modules.UserProfiles.Services
{
    public class UserRolesProfilesService : IUserRolesProfilesService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediaUpload _mediaUpload;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IEmailSender _emailSender;
        public UserRolesProfilesService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext applicationDbContext, UnitOfWork unitOfWork, IMediaUpload mediaUpload, IMapper mapper)
        {
            _unitOfWork= unitOfWork;
            _userManager= userManager;
            _mapper= mapper;
            _mediaUpload = mediaUpload;
            _applicationDbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
            //_emailSender = emailSender;
        }

        public async Task<ResultResponse<PagedList<GetStudentProfileDTO>>> GetAllStudents(StudentTypeParams studentTypeParams)
        {
            var studentsQuery = _applicationDbContext.Students
                .Include(x => x.User)
                .Include(x => x.OpenToRoomate)
               // .Include(x => x.LikedUsers)
                .OrderBy(x => x.CreatedDate)
                .Select(s => new GetStudentProfileDTO
                {
                    FullName = s.User.FirstName + "" + s.User.LastName,
                    Email = s.User.Email,
                    Denomination = s.Denomination,
                    SchoolLevel = s.SchoolLevel,
                    StateOfOrigin = s.StateOfOrigin,
                    Age = s.Age,
                    Religion = s.Religion,
                    Gender = s.Gender,
                    Area = s.Area,
                    //LikedStudentIds = GetMyLikedStudents(s.Id),
                    // await GetAStudentLikedUsers(studentId),
                    Hobby = s.Hobby,
                    IsAvailable = s.IsAvailable,
                    JoinedDate = s.User.CreatedDate,
                    PictureUrl = s.User.ProfilePhotoURL,
                    ProfileViewCount = s.User.ProfileViewCount,
                    Country = s.Country,
                    RoomBudgetAmount = s.OpenToRoomate.RoomBudgetAmount,
                    UserId = s.User.Id
                })
                .Where(x => x.IsAvailable == true)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(studentTypeParams.SearchTerm))
            {
                studentsQuery = studentsQuery.SearchStudent(studentTypeParams.SearchTerm);
            }
           /* else if (studentTypeParams.GottenHostel != null)
            {
                studentsQuery = studentsQuery.Where(o => o.IsAvailable == studentTypeParams.GottenHostel);
            }*/
            else if (studentTypeParams.MinimumPrice != null)
            {
                studentsQuery = studentsQuery.Where(r => r.RoomBudgetAmount >= studentTypeParams.MinimumPrice);
            }
            else if (studentTypeParams.MaximumPrice != null)
            {
                studentsQuery = studentsQuery.Where(x => x.RoomBudgetAmount <= studentTypeParams.MaximumPrice);
            }
            else if(studentTypeParams.Level != null)
            {
                studentsQuery = studentsQuery.Where(x => x.SchoolLevel == studentTypeParams.Level);
            }



            if (studentsQuery is null)
            {
                return ResultResponse<PagedList<GetStudentProfileDTO>>.Failure(ResponseMessages.NotFound);
            }

            return ResultResponse<PagedList<GetStudentProfileDTO>>.Success(await PagedList<GetStudentProfileDTO>.CreateAsync(studentsQuery, studentTypeParams.PageNumber, studentTypeParams.PageSize));

        }

        public async Task<bool> AvailableForRoommate(OpenToRoommateDTO openToRoommateDTO)
        {
            var student = await _unitOfWork.StudentRepository.FindandInclude(x => x.Id == openToRoommateDTO.StudentId,true);
            if (student is null) return false;

            var openToRoomate = OpenToRoommate.CreateOpenToRoomateFactory(openToRoommateDTO.StudentId, openToRoommateDTO.GottenAHostel,
                                openToRoommateDTO.HostelPrice, openToRoommateDTO.HostelAddress);

            student.ToList()[0].IsAvailable = true;

            await _unitOfWork.OpenToRoommateRepository.Add(openToRoomate);
            await _unitOfWork.SaveAsync();

            return true;

        }

        public async Task<bool> CreateReferralCode(string landlordId)
        {
            var result = true;
            var referralInfo = await _unitOfWork.ReferralAgentInfoRepository.Find(x => x.UserId == landlordId.ToString());

            if (referralInfo == null)
            {
                var generatedCode = RandomCodeGenerator.GenerateAlphabetAndNumbers();
                var userreferallInfo = ReferralAgentInfo.CreateUserReferralInfoFactory(landlordId.ToString(), generatedCode);
                await _unitOfWork.ReferralAgentInfoRepository.Add(userreferallInfo);
                var commitState = await _unitOfWork.SaveAsync();
                if(commitState > 0)
                {
                    return true; 
                }
            }
            return false;
        }

        public async Task<GetAllStudentDetailsResponse> GetStudentById(string studentId)
        {
            // var student = await _unitOfWork.StudentRepository.FindandInclude(x => x.Id == studentId && x.User.RolesCSV.Contains(RoleType.Student.GetEnumDescription()), true);
            var student = await _applicationDbContext.Students
                      .Include(x => x.OpenToRoomate)
                      .Include(x => x.LikedUsers)
                      .Include(x => x.User)
                      .FirstOrDefaultAsync(x => x.Id == studentId && x.User.RolesCSV.Contains(RoleType.Student.GetEnumDescription()));
            if (student is null) return null;

            GetAllStudentDetailsResponse studentDetailsResponse = new();

            studentDetailsResponse.UserDto = _mapper.Map<UserDto>(student.User);
            studentDetailsResponse.UserWalletBalanceDTO = _mapper.Map<UserWalletBalanceDTO>(student.User.Wallets);
            studentDetailsResponse.StudentProfile = _mapper.Map<StudentProfile>(student);
            studentDetailsResponse.LikedStudentIds = await GetMyLikedStudents(studentId);
            studentDetailsResponse.StudentLikedIds = await GetAStudentLikedUsers(studentId);

            return studentDetailsResponse;
        }
        public async Task<UpdateStudentResponse> UpdateStudentProfile(UpdateStudentDTO userProfileDTO)
        {
            var studentProfile =  _unitOfWork.StudentRepository.FindandInclude(x => x.Id == userProfileDTO.UserId && x.User.RolesCSV.Contains(RoleType.Student.GetEnumDescription()), true).Result.FirstOrDefault();
            if (studentProfile is null) return null;

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
            studentProfile.GuardianPhoneNumber = userProfileDTO.GuardianPhoneNumber ?? studentProfile.GuardianPhoneNumber;
            studentProfile.LastModifiedDate = DateTime.UtcNow;

              await _unitOfWork.StudentRepository.UpdateAsync(studentProfile);
             var saveState = await _unitOfWork.SaveAsync() > 0;
             if(!saveState) return null;

            return new UpdateStudentResponse()
            {
                UserId = studentProfile.Id,
                FirstName = studentProfile.User.FirstName,
                LastName = studentProfile.User.LastName,
                Age = studentProfile.Age,
                Denomination = studentProfile.Denomination,
                Gender = studentProfile.Gender,
                GuardianPhoneNumber =studentProfile.GuardianPhoneNumber,
                Hobby = studentProfile.Hobby,
                Religion = studentProfile.Religion,
                PhoneNumber = studentProfile.User.PhoneNumber,
                SchoolLevel = studentProfile.SchoolLevel,
                StateOfOrigin = studentProfile.StateOfOrigin,             
            };
        }

        public async Task<bool> CreateStudentProfile(CreateStudentDTO updateStudentProfileDTO)
        {
            var studentProfile = _userManager.Users.Any(x => x.Id == updateStudentProfileDTO.UserId);
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
                GuardianPhoneNumber = updateStudentProfileDTO.GuardianPhoneNumber,
                Country = updateStudentProfileDTO.Country,
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
            };

            var checkIfUserProfileExist = await _unitOfWork.StudentRepository.Find(x => x.Id == updateStudentProfileDTO.UserId && x.User.RolesCSV == RoleString.LandLord);
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
            var landlords = await _applicationDbContext.Landlords
                    .Include(x => x.User)
                    .Include(x => x.Hostels)
                    .ThenInclude(x => x.Rooms)
                    .ToListAsync();

            var landlordMapping = _mapper.Map<List<GetLandlordProfileDTO>>(landlords);

            return landlordMapping;
        }

        public async Task<List<GetAgentProfileDTO>> GetAllAgents()
        {
            var agents = await _unitOfWork.AgentRepository.FindandInclude(x => x.User.RolesCSV.Contains(RoleType.Agent.GetEnumDescription()), true);
            var agentMapping = _mapper.Map<List<GetAgentProfileDTO>>(agents);

            return agentMapping;
        }

        public async Task<GetAllLandlordProfileDetails> GetLandlordsById(string landlordId)
        {
            // var landlord = await _unitOfWork.LandlordRepository.FindandInclude(x => x.Id == landlordId && x.User.RolesCSV.Contains(RoleType.LandLord.GetEnumDescription()), true);
            var landlord = await _applicationDbContext?.Landlords
                     .Include(x => x.User)
                     .Include(x => x.Hostels)
                     .ThenInclude(x => x.Rooms)
                     ?.FirstOrDefaultAsync(x => x.Id == landlordId && x.User.RolesCSV.Contains(RoleType.LandLord.GetEnumDescription()));

            if (landlord is null) return null;

            GetAllLandlordProfileDetails studentDetailsResponse = new();

            studentDetailsResponse.UserDto = _mapper.Map<UserDto>(landlord.User);
            studentDetailsResponse.UserWalletBalanceDTO = _mapper.Map<UserWalletBalanceDTO>(landlord.User.Wallets);
            studentDetailsResponse.landlordProfile = _mapper.Map<LandlordProfile>(landlord);

            return studentDetailsResponse;
        }

        public async Task<GetAllAgentProfileDetailsResponse> GetAgentById(string agentId)
        {
            // var agent = await _unitOfWork.AgentRepository.FindandInclude(x => x.Id == agentId && x.User.RolesCSV.Contains(RoleType.Agent.GetEnumDescription()), true);
            var agent = await _applicationDbContext.Agents
                      .Include(x => x.User)
                      .Include(x => x.LandlordAgents)
                        //.ThenInclude(x => x.Landlord)
                        //    .ThenInclude(x => x.Hostels)
                        //        .ThenInclude(x => x.Rooms)
                      .FirstOrDefaultAsync(x => x.Id == agentId && x.User.RolesCSV.Contains(RoleType.Agent.GetEnumDescription()));

            if (agent is null) return null;

            GetAllAgentProfileDetailsResponse studentDetailsResponse = new();

            studentDetailsResponse.UserDto = _mapper.Map<UserDto>(agent.User);
            studentDetailsResponse.AgentProfile = _mapper.Map<AgentProfile>(agent);
            //studentDetailsResponse.LandlordProfile = _mapper.Map<LandlordProfile>(agent.LandlordAgents.ToList()[0].Landlord);
            return studentDetailsResponse;
        }

        public async Task<bool> CreateLandLordProfile(CreateLandlordDTO landlordProfileDTO)
        {
            var user = await _userManager.FindByIdAsync(landlordProfileDTO.UserId);
            if (user is null) return false;

            var landlord = new Landlord()
            {
                Id = landlordProfileDTO.UserId,
                Religion = landlordProfileDTO.Religion,
                DateOfBirth = landlordProfileDTO.DateOfBirth,
                State = landlordProfileDTO.State,
                Country = landlordProfileDTO.Country,
                Street = landlordProfileDTO.Street,
                Denomination = landlordProfileDTO.Denomination,
                Gender = landlordProfileDTO.Gender,
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
            };

            var checkIfLandlordExist = await _unitOfWork.LandlordRepository.Find(x => x.Id == landlordProfileDTO.UserId && x.User.RolesCSV == RoleString.LandLord);
            if (checkIfLandlordExist is null)
            {
                var referralInfo = await CreateReferralCode(landlordProfileDTO.UserId);
                if (!referralInfo)
                {
                    await _userManager.DeleteAsync(user);
                }

                await _unitOfWork.LandlordRepository.Add(landlord);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CreateAgentProfile(CreateAgentProfileDTO createAgentProfileDTO)
        {
            var user = await _userManager.FindByIdAsync(createAgentProfileDTO.UserId);
            if (user is null) return false;

            var agent = new Agent()
            {
                Id = createAgentProfileDTO.UserId,
                Religion = createAgentProfileDTO.Religion,
                DateOfBirth = createAgentProfileDTO.DateOfBirth,
                State = createAgentProfileDTO.State,
                Country = createAgentProfileDTO.Country,
                Street = createAgentProfileDTO.Street,
                Denomination = createAgentProfileDTO.Denomination,
                Gender = createAgentProfileDTO.Gender,
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,              
            };

            var checkIfAgentExist = await _unitOfWork.AgentRepository.Find(x => x.Id == createAgentProfileDTO.UserId && x.User.RolesCSV == RoleString.Agent);
            if (checkIfAgentExist is null)
            {
                await _unitOfWork.AgentRepository.Add(agent);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                return false;
            }

            return true;
        }

        public async Task<UpdateLandlordResponse> UpdateLandLordProfile(UpdateLandlordDTO landlordProfileDTO)
        {
            var landlordProfile = _unitOfWork.LandlordRepository.FindandInclude(x => x.Id == landlordProfileDTO.UserId, true).Result.SingleOrDefault();
            if (landlordProfile is null) return null;

            landlordProfile.Religion = landlordProfileDTO.Religion ?? landlordProfile.Religion;
            landlordProfile.State = landlordProfileDTO.State ?? landlordProfile.State;
            landlordProfile.DateOfBirth = landlordProfileDTO.DateOfBirth;
            landlordProfile.Country = landlordProfileDTO.Country ?? landlordProfile.Country;
            landlordProfile.User.PhoneNumber = landlordProfileDTO.PhoneNumber ?? landlordProfile.User.PhoneNumber;
            landlordProfile.User.FirstName = landlordProfileDTO.FirstName ?? landlordProfile.User.FirstName;
            landlordProfile.User.LastName = landlordProfileDTO.LastName ?? landlordProfile.User.LastName;
            landlordProfile.Denomination = landlordProfile.Denomination ?? landlordProfile.Denomination;
            landlordProfile.Street = landlordProfile.Street ?? landlordProfile.Street;
            landlordProfile.Gender = landlordProfile.Gender ?? landlordProfile.Gender;
            landlordProfile.LastModifiedDate = DateTime.UtcNow;

            await _unitOfWork.LandlordRepository.UpdateAsync(landlordProfile);
            var saveState = await _unitOfWork.SaveAsync() > 0;
            if (!saveState) return null;

            return new UpdateLandlordResponse()
            {
                UserId = landlordProfile.Id,
                FirstName = landlordProfile.User.FirstName,
                LastName = landlordProfile.User.LastName,
                DateOfBirth = landlordProfile.DateOfBirth,
                Country = landlordProfile.Country,
                Denomination = landlordProfile.Denomination,
                Gender = landlordProfile.Gender,
                PhoneNumber = landlordProfile.User.PhoneNumber,
                Religion = landlordProfile.Religion,
                State = landlordProfile.State,
                Street = landlordProfile.Street
            };
        }

        public async Task<UpdateLandlordResponse> UpdateAgentProfile(UpdateAgentProfileDTO updateAgentProfileDTO)
        {
            var agentProfile = _unitOfWork.AgentRepository.FindandInclude(x => x.Id == updateAgentProfileDTO.UserId, true).Result.SingleOrDefault();
            if (agentProfile is null) return null;

            agentProfile.Religion = updateAgentProfileDTO.Religion ?? agentProfile.Religion;
            agentProfile.State = updateAgentProfileDTO.State ?? agentProfile.State;
            agentProfile.DateOfBirth = updateAgentProfileDTO.DateOfBirth;
            agentProfile.Country = updateAgentProfileDTO.Country ?? agentProfile.Country;
            agentProfile.User.PhoneNumber = updateAgentProfileDTO.PhoneNumber ?? agentProfile.User.PhoneNumber;
            agentProfile.User.FirstName = updateAgentProfileDTO.FirstName ?? agentProfile.User.FirstName;
            agentProfile.User.LastName = updateAgentProfileDTO.LastName ?? agentProfile.User.LastName;
            agentProfile.Denomination = updateAgentProfileDTO.Denomination ?? agentProfile.Denomination;
            agentProfile.Street = updateAgentProfileDTO.Street ?? agentProfile.Street;
            agentProfile.Gender = updateAgentProfileDTO.Gender ?? agentProfile.Gender;
            agentProfile.LastModifiedDate = DateTime.UtcNow;

            await _unitOfWork.AgentRepository.UpdateAsync(agentProfile);
            var saveState = await _unitOfWork.SaveAsync() > 0;
            if (!saveState) return null;

            return new UpdateLandlordResponse()
            {
                UserId = agentProfile.Id,
                FirstName = agentProfile.User.FirstName,
                LastName = agentProfile.User.LastName,
                DateOfBirth = agentProfile.DateOfBirth,
                Country = agentProfile.Country,
                Denomination = agentProfile.Denomination,
                Gender = agentProfile.Gender,
                PhoneNumber = agentProfile.User.PhoneNumber,
                Religion = agentProfile.Religion,
                State = agentProfile.State,
                Street = agentProfile.Street
            };
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

        public async Task<bool> AcceptLandlordInvitation(string agentId, string landlordId)
        {
            var landlord = _unitOfWork.LandlordRepository.FindandInclude(x => x.Id == landlordId, true).Result.FirstOrDefault();
            if (landlord is null)
                return false;

            var agent = _unitOfWork.AgentRepository.FindandInclude(x => x.Id == agentId, true).Result.FirstOrDefault();

            var landlordAgent = new LandlordAgent()
            {
                Agent = agent,
                Landlord = landlord
            };
            agent?.LandlordAgents.Add(landlordAgent);
            landlord.LandlordAgents.Add(landlordAgent);

            await _unitOfWork.SaveAsync();
            
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

        public async Task<BaseRoleResponse> GetCurrentUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
                return null;

            var user = await _userManager.FindByIdAsync(userId);

            switch (user.RolesCSV)
            {
                case "Agent":
                    var agentResponse = await GetAgentById(userId);
                    return agentResponse ?? GetDefaultUserDTO(user);
                case "LandLord":
                    var landlordResponse = await GetLandlordsById(userId);
                    return landlordResponse ?? GetDefaultUserDTO(user);
                case "Student":
                    var studentResponse = await GetStudentById(userId);
                    return studentResponse ?? GetDefaultUserDTO(user);
                default:
                    return null;

            }
           
        }
      
        private BaseRoleResponse GetDefaultUserDTO(ApplicationUser user)
        {
            return new BaseRoleResponse()
            {
                UserDto = new UserDto()
                {
                    UserId = user.Id,
                    RolesCSV = user.RolesCSV,
                    UserName = $"{user.FirstName} {user.LastName}",
                    JoinedDate = user.CreatedDate,
                    PhoneNumber = user.ProfilePhotoURL
                }
            };
        }

        /* public async Task<List<LikedUserDTO>> GetAStudentLikedUsers(string studentId)
         {
             var likedUsers = await _applicationDbContext.StudentLikes
                         .Where(x => x.LikedStudentId == studentId)
                         .Select(x => x.SourceUser)
                         .ToListAsync();

             var mapData = likedUsers.Select(u => new LikedUserDTO
             {
                 Name = $"{u.FirstName} {u.LastName}",
                 UserId = u.Id,
                 ProfilePicture = u.ProfilePhotoURL
             }).ToList();

             return mapData;
         }*/

        public async Task<List<string>> GetAStudentLikedUsers(string studentId)
        {
            var likedUsers = await _applicationDbContext.StudentLikes
                        .Where(x => x.LikedStudentId == studentId)
                        .Select(x => x.SourceUser.Id)
                        .ToListAsync();

            return likedUsers;
        }

        /*public async Task<List<GetMyLikedStudentDTO>> GetMyLikedStudents(string userId)
        {
            var likedStudents = await _applicationDbContext.StudentLikes
                                .Include(x => x.LikedStudent)                             
                                .Where(x => x.SourceUserId == userId)
                                .Select(x => x.LikedStudent)
                                .ToListAsync();

            var mapData = likedStudents.Select(u => new GetMyLikedStudentDTO
            {
                Name = $"{u.User.FirstName} {u.User.LastName}",
                StudentId = u.Id,
                Gender = u.Gender,
                Level = u.SchoolLevel,
                BudgetAmount = u.OpenToRoomate.RoomBudgetAmount,
                LikesCount = u.LikedUsers.Count(x => x.LikedStudentId == u.Id)
            }).ToList();

            return mapData;
        }*/

        public async Task<List<string>> GetMyLikedStudents(string userId)
        {
            var likedStudents = await _applicationDbContext.StudentLikes
                                .Include(x => x.LikedStudent)
                                .Where(x => x.SourceUserId == userId)
                                .Select(x => x.LikedStudent.Id)
                                .ToListAsync();

            return likedStudents;
        }

        public async Task<bool> AddStudentLike(string sourceId, string studentLikeId)
        {
            var sourceUser = await _userManager.FindByIdAsync(sourceId);
            if (sourceUser is null) return false;

            var studentLiked = await _unitOfWork.StudentRepository.FindandInclude(x => x.Id == studentLikeId, true);
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

        public async Task<bool> DeleteUserAccountAsync(string id)
        {
            var userToDelete = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == id);  
            if (userToDelete is null) return false;

            var result = await _userManager.DeleteAsync(userToDelete);
            if(result.Succeeded)
                return true;

            return false;
        }

        public async Task<string> GetLandlordReferralCode(string landlordId)
        {
            var landlord = await _unitOfWork.ReferralAgentInfoRepository.FindandInclude(x => x.UserId == landlordId, true);
            if (landlord is null)
                return null;

            return landlord.ToList()[0].ReferralCode;
        }

        /*public async Task<bool> SendAgentInvitationCode(string agentEmail, string referralCode, string landlordName, string shortNote)
        {

            var message = $"<p><b>Hello, there!</b></p>" +
             $"<p>You got this mail because you're invited by {landlordName} to be an agent on his Fynda App account. The below code is the referral code to sign you up as an Agent.</p><p>" +
             $"<p><b>{referralCode}</b> <br><br><br><br></p>" +
             $"<p>{shortNote}</p>";

            var emailParams = new EmailParameter(agentEmail, $"You Got An Invitation", message);

            await _emailSender.SendEmailAsync(emailParams);

            return true;
        }*/

        public async Task<ApplicationUser> GetEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user;          
        }

    }
}
