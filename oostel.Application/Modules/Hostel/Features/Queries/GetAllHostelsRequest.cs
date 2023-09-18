using MapsterMapper;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.Hostel.Features.Commands;
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
    public class GetAllHostelsRequest : IRequest<APIResponse>
    {

        public sealed class GetAllHostelsRequestCommand : IRequestHandler<GetAllHostelsRequest, APIResponse>
        {
            private readonly IHostelService _hostelService;
            public GetAllHostelsRequestCommand(IHostelService hostelService) => _hostelService = hostelService;

            public async Task<APIResponse> Handle(GetAllHostelsRequest request, CancellationToken cancellationToken)
            {
                var hostels = await _hostelService.GetAllHostels();

                if (hostels is null) 
                    return APIResponse.GetFailureMessage(HttpStatusCode.OK, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: hostels, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
