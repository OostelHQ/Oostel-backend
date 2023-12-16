using Oostel.Application.Modules.Hostel.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.Features.Queries
{
    public class GetMyLikedHostelsRequest : IRequest<APIResponse>
    {
        public string UserId { get; set; }

        public sealed class GetMyLikedHostelsRequestCommand : IRequestHandler<GetMyLikedHostelsRequest, APIResponse>
        {
            private readonly IHostelService _hostelService;
            public GetMyLikedHostelsRequestCommand(IHostelService hostelService) => _hostelService = hostelService;

            public async Task<APIResponse> Handle(GetMyLikedHostelsRequest request, CancellationToken cancellationToken)
            {
                var LikedHostels = await _hostelService.GetMyLikedHostels(request.UserId);

                if (LikedHostels is null)
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: LikedHostels, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
