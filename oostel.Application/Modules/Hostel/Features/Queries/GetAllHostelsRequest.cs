using MapsterMapper;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.Hostel.Features.Commands;
using Oostel.Application.Modules.Hostel.Services;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.Features.Queries
{
    public class GetAllHostelsRequest : IRequest<ResultResponse<PagedList<HostelsResponse>>>
    {
        public HostelTypesParam? hostelTypesParam { get; set; }
        public sealed class GetAllHostelsRequestCommand : IRequestHandler<GetAllHostelsRequest, ResultResponse<PagedList<HostelsResponse>>>
        {
            private readonly IHostelService _hostelService;
            public GetAllHostelsRequestCommand(IHostelService hostelService) => _hostelService = hostelService;

            public async Task<ResultResponse<PagedList<HostelsResponse>>> Handle(GetAllHostelsRequest request, CancellationToken cancellationToken)
            {
                var hostels = await _hostelService.GetAllHostels(request.hostelTypesParam);

                if (hostels is null) 
                    return ResultResponse<PagedList<HostelsResponse>>.Failure(ResponseMessages.NotFound);

                return ResultResponse<PagedList<HostelsResponse>>.Success(hostels.Data);
            }
        }
    }
}
