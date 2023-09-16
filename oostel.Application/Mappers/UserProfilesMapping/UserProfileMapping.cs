using Mapster;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Domain.UserProfiles.Entities;

namespace Oostel.Application.Mappers.UserProfilesMapping
{
    public class UserProfileMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserProfile, UserProfileDTO>();
            config.NewConfig<UserProfile, GetUserProfileDTO>()
                .Map(dest => dest.UserId, src => src.Id)
                .Map(dest => dest.PictureUrl, src => src.ProfilePhotoURL)
                .Map(dest => dest.FullName, src => $"{src.User.FirstName} {src.User.LastName}");
        }
    }
}
