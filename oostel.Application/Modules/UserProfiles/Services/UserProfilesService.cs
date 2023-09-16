using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserProfiles.Entities;
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
        public UserProfilesService(UserManager<ApplicationUser> userManager, UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork= unitOfWork;
            _userManager= userManager;
            _mapper= mapper;
        }

        public async Task<List<UserProfileDTO>> GetAllUserProfile()
        {
            var userProfile = await _unitOfWork.UserProfileRepository.GetAll(true);
            var userProfileMapping = _mapper.Map<List<UserProfileDTO>>(userProfile);

            return userProfileMapping;
        }

        public async Task<UserProfileDTO> GetUserProfileById(string id)
        {
            var userProfile = await _unitOfWork.UserProfileRepository.GetById(id);
            if (userProfile is null) return null;

            var userProfileMapping = _mapper.Map<UserProfileDTO>(userProfile);
            return userProfileMapping;
        }
        public async Task<bool> UpdateUserProfile(UserProfileDTO userProfileDTO)
        {
            var userProfile = await _unitOfWork.UserProfileRepository.Find(x => x.Id == userProfileDTO.UserId);
            if (userProfile is null) return false;

            userProfile.SchoolLevel = userProfileDTO.SchoolLevel ?? userProfile.SchoolLevel;
            userProfile.Age = userProfileDTO.Age;
            userProfile.Hobby = userProfileDTO.Hobby ?? userProfile.Hobby;
            userProfile.Gender = userProfileDTO.Gender ?? userProfile.Gender ;
            userProfile.Religion = userProfileDTO.Religion ?? userProfile.Religion ;
            userProfile.StateOfOrigin = userProfileDTO.StateOfOrigin ?? userProfile.StateOfOrigin ;
            userProfile.User.FirstName = userProfileDTO.FirstName ?? userProfile.User.FirstName;
            userProfile.User.LastName = userProfileDTO.LastName ?? userProfile.User.LastName;
            userProfile.LastModifiedDate = DateTime.UtcNow;

              await _unitOfWork.UserProfileRepository.UpdateAsync(userProfile);
             var saveState = await _unitOfWork.SaveAsync() > 0;
             if(!saveState) return false;

            return true;
        }

        public async Task<bool> CreateUserProfile(UserProfileDTO userProfileDTO)
        {
            var user = await _userManager.FindByIdAsync(userProfileDTO.UserId);
            if (user is null) return false;

            var userProfile = new UserProfile()
            {
                Id = user.Id,
                Age = userProfileDTO.Age,
                Gender = userProfileDTO.Gender,
                Hobby = userProfileDTO.Hobby,
                Religion = userProfileDTO.Religion,
                SchoolLevel = userProfileDTO.SchoolLevel,
                StateOfOrigin = userProfileDTO.StateOfOrigin,
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
            };

            var checkIfUserProfileExist = await _unitOfWork.UserProfileRepository.Find(x => x.Id == userProfileDTO.UserId);
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
    }
}
