using Mapster;
using Microsoft.Extensions.Hosting;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Domain.Hostel.Entities;

namespace Oostel.Application.Mappers.HostelMapping
{
    public class HostelMapping : IRegister
    {
       
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Hostel, HostelDTO>();
            config.NewConfig<Hostel, HostelDetails>()
                .Map(dest => dest.Country, src => src.Country)
                .Map(dest => dest.HomeSize, src => src.HomeSize)
               // .Map(dest => dest.HostelLikesCount, src => src.HostelLikes.Count(x => x.LikedHostelId == hostel.Id))
                .Map(dest => dest.Country, src => src.Country)
                .Map(dest => dest.Country, src => src.Country);
            config.NewConfig<Room, RoomDTO>()
                .Map(dest => dest.Files, src => src.RoomPictures);



            config.NewConfig<HostelFacilities, HostelFacilitiesDTO>()
                .Map(dest => dest.FacilityName, src => src.FacilityName);
            config.NewConfig<HostelRulesAndRegulations, HostelRulesAndRegulationsDTO>()
                .Map(dest => dest.RuleAndRegulation, src => src.RuleAndRegulation);

            config.NewConfig<RoomFacilities, RoomFacilitiesDTO>()
                .Map(dest => dest.FacilityName, src => src.FacilityName);
            config.NewConfig<RoomPictures, RoomPicturesDTO>()
                .Map(dest => dest.PictureUrl, src => src.PictureUrl);

            config.NewConfig<Hostel, RoomDTO>()
                .Map(dest => dest.Files, src => src.Rooms.ToList()[0].RoomPictures);

            config.NewConfig<Room, HostelDetailsResponse>();
            config.NewConfig<Room, RoomToReturn>();
            config.NewConfig<Room, RoomCollectionsDTO>()
                .Map(dest => dest.HostelId, src => src.HostelId)
                .Map(dest => dest.RoomId, src => src.Id)
                .Map(dest => dest.RoomFacilities, src => src.RoomFacilities)
                .Map(dest => dest.Files, src => src.RoomPictures);

            config.NewConfig<RoomToCreate, RoomCollectionsDTO>();

            config.NewConfig<Room, HostelsResponse>()
                .Map(dest => dest.HostelId, src => src.Id)
                .Map(dest => dest.NumberOfRoomsLeft, src => src.IsRented);


            config.NewConfig<Comment, CreateCommentDTO>();
            config.NewConfig<Comment, CommentDTO>();
        }
    }
}
