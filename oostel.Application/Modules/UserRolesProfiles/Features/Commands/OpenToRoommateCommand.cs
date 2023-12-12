using MapsterMapper;
using Oostel.Application.Modules.UserProfiles.Features.Commands;
using Oostel.Application.Modules.UserProfiles.Services;
using Oostel.Application.Modules.UserRolesProfiles.DTOs;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserRolesProfiles.Features.Commands
{
    public class OpenToRoommateCommand : IRequest<APIResponse>
    {
        public string StudentId { get; set; }
        public bool GottenAHostel { get; set; }
        public decimal HostelPrice { get; set; }
        public string HostelAddress { get; set; }

        public sealed class OpenToRoommateCommandHandler : IRequestHandler<OpenToRoommateCommand, APIResponse>
        {
            private readonly IUserRolesProfilesService _userRolesProfilesService;
            private readonly IMapper _mapper;
            public OpenToRoommateCommandHandler(IUserRolesProfilesService userRolesProfilesService, IMapper mapper)
            {
                _userRolesProfilesService = userRolesProfilesService;
                _mapper = mapper;
            }
            public async Task<APIResponse> Handle(OpenToRoommateCommand request, CancellationToken cancellationToken)
            {
                var mapData = _mapper.Map<OpenToRoommateDTO>(request);
                var openToRoommate = await _userRolesProfilesService.AvailableForRoommate(mapData);
                if (!openToRoommate) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: null, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
