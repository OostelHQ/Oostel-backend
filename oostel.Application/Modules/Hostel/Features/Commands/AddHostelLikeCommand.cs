using MapsterMapper;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.Hostel.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.Features.Commands
{
    public class AddHostelLikeCommand : IRequest<APIResponse>
    {
        public string HostelLikeId { get; set; }
        public string LikingUserId { get; set; }
        public sealed class AddHostelLikeCommandHandler : IRequestHandler<AddHostelLikeCommand, APIResponse>
        {
            private readonly IHostelService _hostelService;
            public AddHostelLikeCommandHandler(IHostelService hostelService, IMapper mapper) =>
                _hostelService = hostelService;

            public async Task<APIResponse> Handle(AddHostelLikeCommand request, CancellationToken cancellationToken)
            {
                var createhostel = await _hostelService.AddHostelLike(request.HostelLikeId, request.LikingUserId);

                if (!createhostel) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: null, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
