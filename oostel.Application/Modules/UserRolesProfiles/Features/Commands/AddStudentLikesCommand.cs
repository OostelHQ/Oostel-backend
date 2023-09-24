using MapsterMapper;
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

namespace Oostel.Application.Modules.UserRolesProfiles.Features.Commands
{
    public class AddStudentLikesCommand: IRequest<APIResponse>
    {
        public string LikingUserId { get; set; }
        public string StudentLikeId { get; set; }
        public sealed class AddStudentLikesCommandHandler : IRequestHandler<AddStudentLikesCommand, APIResponse>
        {
            private readonly IHostelService _hostelService;
            public AddStudentLikesCommandHandler(IHostelService hostelService, IMapper mapper) =>
                _hostelService = hostelService;

            public async Task<APIResponse> Handle(AddStudentLikesCommand request, CancellationToken cancellationToken)
            {
                var likeStudent = await _hostelService.AddHostelLike(request.LikingUserId, request.StudentLikeId);

                if (!likeStudent) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: null, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
