using Mapster;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Domain.UserRoleProfiles.Entities;

namespace Oostel.Application.Mappers.UserProfilesMapping
{
    public class UserProfileMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Student, StudentProfileDTO>();
            config.NewConfig<Student, GetStudentProfileDTO>()
                .Map(dest => dest.UserId, src => src.Id)
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest.JoinedDate, src => src.User.CreatedDate)
                .Map(dest => dest.PictureUrl, src => src.ProfilePhotoURL)
                .Map(dest => dest.FullName, src => $"{src.User.FirstName} {src.User.LastName}");


            config.NewConfig<Landlord, LandlordProfileDTO>();
            config.NewConfig<Landlord, GetLandlordProfileDTO>()
                .Map(dest => dest.UserId, src => src.Id)
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest.JoinedDate, src => src.User.CreatedDate)
                .Map(dest => dest.PictureUrl, src => src.ProfilePhotoURL)
                .Map(dest => dest.FullName, src => $"{src.User.FirstName} {src.User.LastName}");
        }
    }
}
