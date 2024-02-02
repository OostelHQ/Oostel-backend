using MapsterMapper;
using Oostel.Application.Modules.Hostel.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.Features.Commands
{
    public class DeleteHostelPictureCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }
        public string HostelPictureId { get; set; }

        public sealed class DeleteHostelPictureCommandHandler : IRequestHandler<DeleteHostelPictureCommand, APIResponse>
        {
            private readonly IHostelService _hostelService;
            public DeleteHostelPictureCommandHandler(IHostelService hostelService) =>
                _hostelService = hostelService;

            public async Task<APIResponse> Handle(DeleteHostelPictureCommand request, CancellationToken cancellationToken)
            {
                var createhostel = await _hostelService.DeleteProductPicture(request.UserId, request.HostelPictureId);

                if (!createhostel) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: null, ResponseMessages.DeleteMessage);
            }
        }
    }
}
