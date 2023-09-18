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
    public class GetARoomForHostelRequest : IRequest<APIResponse>
    {
        public string HostelId { get; set; }
        public string RoomId { get; set; }

        public sealed class GetARoomForHosteRequestCommand : IRequestHandler<GetARoomForHostelRequest, APIResponse>
        {
            private readonly IHostelService _hostelService;
            public GetARoomForHosteRequestCommand(IHostelService hostelService) => _hostelService = hostelService;

            public async Task<APIResponse> Handle(GetARoomForHostelRequest request, CancellationToken cancellationToken)
            {
                var room = await _hostelService.GetARoomForHostel(request.HostelId, request.RoomId);

                if (room is null)
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: room, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
