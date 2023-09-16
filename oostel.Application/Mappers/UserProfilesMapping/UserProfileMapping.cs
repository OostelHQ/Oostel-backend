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
        }
    }
}
