using MapsterMapper;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.Hostel.Services;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Application.Modules.UserProfiles.Features.Commands;
using Oostel.Application.Modules.UserProfiles.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using Oostel.Domain.Hostel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.Features.Commands
{
    public class CreateHostelCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }
        public string HostelName { get; set; }
        public string HostelDescription { get; set; }
        public int TotalRoom { get; set; }
        public decimal HomeSize { get; set; }
        public string Street { get; set; }
        public string Junction { get; set; }
        public HostelCategory HostelCategory { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public ICollection<RoomToCreate>? Rooms { get; set; }
        public List<string> RulesAndRegulation { get; set; }
        public List<string> HostelFacilities { get; set; }
        public bool IsAnyRoomVacant { get; set; }

        public sealed class CreateHostelCommandHandler : IRequestHandler<CreateHostelCommand, APIResponse>
        {
            private readonly IHostelService _hostelService;
            private readonly IMapper _mapper;
            public CreateHostelCommandHandler(IHostelService hostelService, IMapper mapper)
            {
                _hostelService = hostelService;
                _mapper = mapper;
            }
            public async Task<APIResponse> Handle(CreateHostelCommand request, CancellationToken cancellationToken)
            {
                var mapData = _mapper.Map<HostelDTO>(request);
                var createhostel = await _hostelService.CreateHostel(mapData);

                if (!createhostel) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: null, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
