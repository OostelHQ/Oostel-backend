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
    public class GetHostelByIdRequest : IRequest<APIResponse>
    {
        public string HostelId { get; set; }

        public sealed class GetHostelByIdRequestCommand : IRequestHandler<GetHostelByIdRequest, APIResponse>
        {
            private readonly IHostelService _hostelService;
            public GetHostelByIdRequestCommand(IHostelService hostelService) => _hostelService = hostelService;

            public async Task<APIResponse> Handle(GetHostelByIdRequest request, CancellationToken cancellationToken)
            {
                var hostel = await _hostelService.GetHostelById(request.HostelId);

                if (hostel is null)
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: hostel, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
