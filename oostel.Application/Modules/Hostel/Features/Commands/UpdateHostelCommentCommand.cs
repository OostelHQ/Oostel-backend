using Oostel.Application.Modules.Hostel.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using Oostel.Infrastructure.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.Features.Commands
{
    public class UpdateHostelCommentCommand : IRequest<APIResponse>
    {
        public string CommentId { get; set; }
        public string HostelId { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public string? ParentCommentId { get; set; }

        public sealed class UpdateHostelCommentCommandHandler : IRequestHandler<UpdateHostelCommentCommand, APIResponse>
        {
            private readonly IHostelService _hostelService;
            private readonly IMediaUpload _mediaUpload;
            public UpdateHostelCommentCommandHandler(IHostelService hostelService, IMediaUpload mediaUpload)
            {
                _hostelService = hostelService;
                _mediaUpload = mediaUpload;
            }

            public async Task<APIResponse> Handle(UpdateHostelCommentCommand request, CancellationToken cancellationToken)
            {
                var getHostelComment = await _hostelService.GetHostelCommentByCommentIdAsync(request.CommentId);
                if (getHostelComment != null)
                {
                    var hostel = await _hostelService.UpdateHostelCommentAsync(new Domain.Hostel.Entities.Comment
                    {
                        LastModifiedDate = DateTime.UtcNow,
                        CommenterId = request.UserId,
                        ParentCommentId = request.ParentCommentId,
                        UserComment = request.Comment,
                        Id = request.CommentId,
                        HostelId = request.HostelId,

                    });
                    return APIResponse.GetSuccessMessage(System.Net.HttpStatusCode.Created, hostel, ResponseMessages.SuccessfulCreation);
                }
                return APIResponse.GetFailureMessage(System.Net.HttpStatusCode.BadRequest, null, ResponseMessages.NotFound);
            }
        }
    }
}
