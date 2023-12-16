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
    public class GetHostelLikedUsersRequest : IRequest<APIResponse>
    {
        public string HostelId { get; set; }

        public sealed class GetHostelLikedUsersRequestCommand : IRequestHandler<GetHostelLikedUsersRequest, APIResponse>
        {
            private readonly IHostelService _hostelService;
            public GetHostelLikedUsersRequestCommand(IHostelService hostelService) => _hostelService = hostelService;

            public async Task<APIResponse> Handle(GetHostelLikedUsersRequest request, CancellationToken cancellationToken)
            {
                var LikedUsers = await _hostelService.GetHostelLikedUsers(request.HostelId);

                if (LikedUsers is null)
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: LikedUsers, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
