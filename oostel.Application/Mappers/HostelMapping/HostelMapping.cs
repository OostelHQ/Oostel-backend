using Mapster;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Domain.Hostel.Entities;

namespace Oostel.Application.Mappers.HostelMapping
{
    public class HostelMapping : IRegister
    {
       
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Hostel, HostelDTO>();
            config.NewConfig<Room, RoomDTO>()
                .Map(dest => dest.Files, src => src.RoomPictures);

            config.NewConfig<Hostel, RoomDTO>()
                //.Map(dest =)
                .Map(dest => dest.Files, src => src.Rooms.ToList()[0].RoomPictures);

            config.NewConfig<Room, AHostelResponse>();

            config.NewConfig<Room, HostelsResponse>()
                .Map(dest => dest.HostelId, src => src.Id)
                .Map(dest => dest.NumberOfRoomsLeft, src => src.IsRented);

            config.NewConfig<Comment, CreateCommentDTO>();
            config.NewConfig<Comment, CommentDTO>();
        }
    }
}
