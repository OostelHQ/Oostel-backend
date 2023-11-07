using Mapster;
using Oostel.API.ViewModels.HostelsVM;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.Hostel.Features.Commands;
using Oostel.Domain.Hostel.Entities;

namespace Oostel.API.Mappings
{
    public class HostelMappers : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateHostelCommand, HostelRequest>()
                .Map(dest => dest.HostelFrontViewPicture, src => src.HostelFrontViewPicture);
           
        }

    }
}
