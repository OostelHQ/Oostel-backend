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
    public class GetMyHostelsRequest : IRequest<APIResponse>
    {
        public string LandlordId { get; set; }

        public sealed class GetMyHostelsRequestCommand : IRequestHandler<GetMyHostelsRequest, APIResponse>
        {
            private readonly IHostelService _hostelService;
            public GetMyHostelsRequestCommand(IHostelService hostelService) => _hostelService = hostelService;

            public async Task<APIResponse> Handle(GetMyHostelsRequest request, CancellationToken cancellationToken)
            {
                var myHostels = await _hostelService.GetMyHostels(request.LandlordId);

                if (myHostels is null)
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: myHostels, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
