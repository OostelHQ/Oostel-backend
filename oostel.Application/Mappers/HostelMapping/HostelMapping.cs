using Mapster;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Domain.Hostel.Entities;
using Oostel.Domain.UserProfiles.Entities;
using Oostel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
