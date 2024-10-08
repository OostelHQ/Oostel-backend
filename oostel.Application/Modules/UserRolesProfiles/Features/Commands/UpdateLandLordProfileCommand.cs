﻿using MapsterMapper;
using Oostel.Application.Modules.UserProfiles.DTOs;
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

namespace Oostel.Application.Modules.UserProfiles.Features.Commands
{
    public class UpdateLandLordProfileCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string State { get; set; }
        public string? Denomination { get; set; }
        public string? Gender { get; set; }
        public string? Street { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Religion { get; set; }

        public sealed class UpdateLandLordProfileCommandHandler : IRequestHandler<UpdateLandLordProfileCommand, APIResponse>
        {
            private readonly IUserRolesProfilesService _userProfilesService;
            private readonly IMapper _mapper;
            public UpdateLandLordProfileCommandHandler(IUserRolesProfilesService userProfilesService, IMapper mapper)
            {
                _userProfilesService = userProfilesService;
                _mapper = mapper;
            }
            public async Task<APIResponse> Handle(UpdateLandLordProfileCommand request, CancellationToken cancellationToken)
            {
                var mapData = _mapper.Map<UpdateLandlordDTO>(request);
                var landlordProfile = await _userProfilesService.UpdateLandLordProfile(mapData);
                if (landlordProfile is null) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailToUpdateError);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: landlordProfile, ResponseMessages.UpdateMessage);
            }
        }
    }
}
