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
    public class GetAllRoomsForHostelRequest :IRequest<APIResponse>
    {
        public string HostelId { get; set; }

        public sealed class GetAllRoomsForHostelRequestCommand : IRequestHandler<GetAllRoomsForHostelRequest, APIResponse>
        {
            private readonly IHostelService _hostelService;
            public GetAllRoomsForHostelRequestCommand(IHostelService hostelService) => _hostelService = hostelService;

            public async Task<APIResponse> Handle(GetAllRoomsForHostelRequest request, CancellationToken cancellationToken)
            {
                var rooms = await _hostelService.GetAllRoomsForHostel(request.HostelId);

                if (rooms is null)
                    return APIResponse.GetFailureMessage(HttpStatusCode.OK, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: null, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
