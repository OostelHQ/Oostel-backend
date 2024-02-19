using MapsterMapper;
using Microsoft.Extensions.Hosting;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.Hostel.Services;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
using Oostel.Common.Types;
using Oostel.Domain.Hostel.Entities;

namespace Oostel.Application.Modules.Hostel.Features.Commands
{
    public class CreateCommentCommand : IRequest<APIResponse>
    {
        public string UserComment { get; set; }
        public string UserId { get; set; }
        public string HostelId { get; set; }
        public string? ParentCommentId { get; set; }

        public sealed class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, APIResponse>
        {
            private readonly IHostelService _hostelService;
            private readonly IMapper _mapper;
            public CreateCommentCommandHandler(IHostelService hostelService, IMapper mapper)
            {
                _hostelService = hostelService;
                _mapper = mapper;
            }

            public async Task<APIResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
            {
                var comment = await _hostelService.CreateHostelCommentAsync(new Comment()
                {
                    CommenterId = request.UserId,
                    HostelId = request.HostelId,
                    UserComment = request.UserComment,
                    ParentCommentId = request.ParentCommentId
                });

                return APIResponse.GetSuccessMessage(System.Net.HttpStatusCode.Created, comment, ResponseMessages.SuccessfulCreation);
            }
           
        }
    }
}
