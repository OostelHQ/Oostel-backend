using MapsterMapper;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.Hostel.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.Features.Commands
{
    public class CreateRoomCollectionCommand : IRequest<APIResponse>
    {
        public string LandlordId { get; set; }
        public string HostelId { get; set; }
        public IEnumerable<RoomToCreate> roomToCreates { get; set; }

        public sealed class CreateRoomCollectionCommandHandler : IRequestHandler<CreateRoomCollectionCommand, APIResponse>
        {
            private readonly IHostelService _hostelService;
            private readonly IMapper _mapper;
            public CreateRoomCollectionCommandHandler(IHostelService hostelService, IMapper mapper)
            {
                _hostelService = hostelService;
                _mapper = mapper;
            }

            public async Task<APIResponse> Handle(CreateRoomCollectionCommand request, CancellationToken cancellationToken)
            {

                var roomData = await _hostelService.CreateRoomCollectionAsync(request.LandlordId, request.HostelId, request.roomToCreates);
              
                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: roomData, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
