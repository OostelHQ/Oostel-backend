using Oostel.Application.Modules.Hostel.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.Features.Commands
{
    public class DeleteHostelCommentCommand : IRequest<APIResponse>
    {
        public string CommentId { get; set; }

        public sealed class DeletePostCommentCommandHandler : IRequestHandler<DeleteHostelCommentCommand, APIResponse>
        {
            private readonly IHostelService _hostelService;
            public DeletePostCommentCommandHandler(IHostelService hostelService)
            {
                _hostelService = hostelService;
            }

            public async Task<APIResponse> Handle(DeleteHostelCommentCommand request, CancellationToken cancellationToken)
            {
                var getHostel = await _hostelService.GetHostelCommentByCommentIdAsync(request.CommentId);
                if (getHostel != null)
                {
                    var hostel = await _hostelService.DeleteHostelCommentAsync(request.CommentId, getHostel.HostelId);
                    return APIResponse.GetSuccessMessage(System.Net.HttpStatusCode.Created, hostel, ResponseMessages.DeleteMessage);
                }
                return APIResponse.GetFailureMessage(System.Net.HttpStatusCode.BadRequest, null, ResponseMessages.NotFound);
            }
        }
    }
}
