using Mapster;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Application.Modules.UserRolesProfiles.DTOs;
using Oostel.Application.Modules.UserWallet.DTOs;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserRoleProfiles.Entities;
using Oostel.Domain.UserRolesProfiles.Entities;
using Oostel.Domain.UserWallet;

namespace Oostel.Application.Mappers.UserProfilesMapping
{
    public class UserProfileMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Student, StudentProfileDTO>();
            config.NewConfig<Student, UpdateStudentDTO>();
            config.NewConfig<Wallet, UserWalletBalanceDTO>();
            config.NewConfig<ApplicationUser, UserDto>()
                .Map(dest => dest.UserId, src => src.Id)
                .Map(dest => dest.JoinedDate, src => src.CreatedDate);

            config.NewConfig<Student, GetStudentProfileDTO>()
                .Map(dest => dest.UserId, src => src.Id)
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest.JoinedDate, src => src.User.CreatedDate)
                .Map(dest => dest.PictureUrl, src => src.User.ProfilePhotoURL)
                .Map(dest => dest.IsAvailable, src => src.IsAvailable)
                .Map(dest => dest.RoomBudgetAmount, src => src.OpenToRoomate.RoomBudgetAmount)
                .Map(dest => dest.ProfileViewCount, src => src.User.ProfileViewCount)
                .Map(dest => dest.FullName, src => $"{src.User.FirstName} {src.User.LastName}");

            config.NewConfig<Student, StudentProfile>()
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest.PictureUrl, src => src.User.ProfilePhotoURL)
                .Map(dest => dest.IsAvailable, src => src.IsAvailable)
                .Map(dest => dest.GuardianPhoneNumber, src => src.GuardianPhoneNumber)
                .Map(dest => dest.RoomBudgetAmount, src => src.OpenToRoomate.RoomBudgetAmount)
                .Map(dest => dest.ProfileViewCount, src => src.User.ProfileViewCount)
                .Map(dest => dest.FullName, src => $"{src.User.FirstName} {src.User.LastName}");


            config.NewConfig<Landlord, LandlordProfileDTO>();
            config.NewConfig<Landlord, LandlordProfileToDisplay>()
                .Map(dest => dest.LandlordId, src => src.Id)
                .Map(dest => dest.RegisterdOn, src => src.User.CreatedDate)
                .Map(dest => dest.Location, src => src.Street)
                .Map(dest => dest.ProfilePicture, src => src.User.ProfilePhotoURL)
                .Map(dest => dest.FullName, src => $"{src.User.FirstName} {src.User.LastName}")
                .Map(dest => dest.RoleCVS, src => src.User.RolesCSV);

            config.NewConfig<Landlord, UpdateLandlordDTO>();
            config.NewConfig<Landlord, GetLandlordProfileDTO>()
                .Map(dest => dest.UserId, src => src.Id)
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest.NumberOfHostelsCreated, src => src.Hostels.Count(x => x.LandlordId == src.Id))
                .Map(dest => dest.NumberRoomsCreated, src => src.Hostels.Sum(hostel => hostel.Rooms.Count))
                .Map(dest => dest.JoinedDate, src => src.User.CreatedDate)
                .Map(dest => dest.ProfileViewCount, src => src.User.ProfileViewCount)
                .Map(dest => dest.PictureUrl, src => src.User.ProfilePhotoURL)
                .Map(dest => dest.FullName, src => $"{src.User.FirstName} {src.User.LastName}");

            config.NewConfig<Landlord, LandlordProfile>()
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest.NumberOfHostelsCreated, src => src.Hostels.Count(x => x.LandlordId == src.Id))
                .Map(dest => dest.PictureUrl, src => src.User.ProfilePhotoURL)
                .Map(dest => dest.NumberRoomsCreated, src => src.Hostels.Sum(hostel => hostel.Rooms.Count))
                .Map(dest => dest.FirstName, src => src.User.FirstName)
                .Map(dest => dest.LastName, src => src.User.LastName)
                .Map(dest => dest.ProfileViewCount, src => src.User.ProfileViewCount);
                

            config.NewConfig<Agent, AgentProfileDTO>();
            config.NewConfig<Agent, UpdateAgentProfileDTO>();
            config.NewConfig<Agent, AgentProfileToDisplay>()
                .Map(dest => dest.AgentId, src => src.Id)
                .Map(dest => dest.RegisterdOn, src => src.User.CreatedDate)
                .Map(dest => dest.Location, src => src.Street)
                .Map(dest => dest.ProfilePicture, src => src.User.ProfilePhotoURL)
                .Map(dest => dest.RoleCVS, src => src.User.RolesCSV)
                .Map(dest => dest.FullName, src => $"{src.User.FirstName} {src.User.LastName}");
                
                   
            config.NewConfig<Agent, GetAgentProfileDTO>()
                .Map(dest => dest.UserId, src => src.Id)
                .Map(dest => dest.Email, src => src.User.Email)
                //.Map(dest => dest.NumberOfHostelsCreated, src => src.Hostels.Count(x => x.UserId == src.Id))
                //.Map(dest => dest.NumberRoomsCreated, src => src.Hostels.FirstOrDefault().Rooms.Count(x => x.Id == src.Id))
                .Map(dest => dest.JoinedDate, src => src.User.CreatedDate)
                .Map(dest => dest.ProfileViewCount, src => src.User.ProfileViewCount)
                .Map(dest => dest.PictureUrl, src => src.User.ProfilePhotoURL)
                .Map(dest => dest.FullName, src => $"{src.User.FirstName} {src.User.LastName}");

            config.NewConfig<Agent, AgentProfile>()
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest.PictureUrl, src => src.User.ProfilePhotoURL)
                .Map(dest => dest.LastName, src => src.User.LastName)
                .Map(dest => dest.FirstName, src => src.User.FirstName)
                .Map(dest => dest.ProfileViewCount, src => src.User.ProfileViewCount);

            config.NewConfig<OpenToRoommate, OpenToRoommateDTO>();
        }
    }
}
