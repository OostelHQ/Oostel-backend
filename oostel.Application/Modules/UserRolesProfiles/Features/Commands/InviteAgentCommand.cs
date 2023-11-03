using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Oostel.Application.Modules.UserAuthentication.Features.Commands;
using Oostel.Application.Modules.UserAuthentication.Services;
using Oostel.Application.Modules.UserProfiles.Services;
using Oostel.Application.Modules.UserRolesProfiles.DTOs;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserRolesProfiles.Features.Commands
{
    public class InviteAgentCommand : IRequest<APIResponse>
    {
        public string LandLordId { get; set; }
        public string AgentEmail { get; set; }
        public string ShortNote { get; set; }

        public sealed class InviteAgentCommandHandler : IRequestHandler<InviteAgentCommand, APIResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ILogger<InviteAgentCommandHandler> _logger;
            private readonly IUserRolesProfilesService _userRolesProfilesService;
            private readonly UnitOfWork _unitOfWork;

            public InviteAgentCommandHandler(UserManager<ApplicationUser> userManager, ILogger<InviteAgentCommandHandler> logger,
                IUserRolesProfilesService userRolesProfilesService, UnitOfWork unitOfWork)
            {
                _userManager= userManager;
                _logger= logger;
                _userRolesProfilesService= userRolesProfilesService;
                _unitOfWork = unitOfWork;
            }

            public async Task<APIResponse> Handle(InviteAgentCommand request, CancellationToken cancellationToken)
            {
                var referralCode = await _unitOfWork.ReferralAgentInfoRepository.FindandInclude(x => x.UserId == request.LandLordId, true);
                if (referralCode is null)
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.NotFound);

                 await _userRolesProfilesService.SendAgentInvitationCode(request.AgentEmail, referralCode.ToList()[0].ReferralCode, referralCode.ToList()[0].User.FirstName, request.ShortNote);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: null, ResponseMessages.InvitationMessage);
            }
        }
    }
}
