using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.Hostel.Services;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.Features.Queries
{
    public class GetCommentsRequest : IRequest<ResultResponse<List<CommentDTO>>>
    {
        public string HostelId { get; set; }

        public sealed class GetCommentsRequestCommand : IRequestHandler<GetCommentsRequest, ResultResponse<List<CommentDTO>>>
        {
            private readonly IHostelService _hostelService;
            public GetCommentsRequestCommand(IHostelService hostelService) => _hostelService = hostelService;

            public async Task<ResultResponse<List<CommentDTO>>> Handle(GetCommentsRequest request, CancellationToken cancellationToken)
            {
                var comments = await _hostelService.GetComments(request.HostelId);

                if (comments is null)
                    return ResultResponse<List<CommentDTO>>.Failure(ResponseMessages.NotFound);

                return ResultResponse<List<CommentDTO>>.Success(comments.Data);
            }
        }
    }
}
