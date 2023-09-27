using MapsterMapper;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.Hostel.Services;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;

namespace Oostel.Application.Modules.Hostel.Features.Commands
{
    public class CreateCommentCommand : IRequest<ResultResponse<CommentDTO>>
    {
        public string UserComment { get; set; }
        public string HostelId { get; set; }


        public sealed class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, ResultResponse<CommentDTO>>
        {
            private readonly IHostelService _hostelService;
            private readonly IMapper _mapper;
            public CreateCommentCommandHandler(IHostelService hostelService, IMapper mapper)
            {
                _hostelService = hostelService;
                _mapper = mapper;
            }
            public async Task<ResultResponse<CommentDTO>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
            {
                var mapData = _mapper.Map<CreateCommentDTO>(request);
                var createComment = await _hostelService.CreateComment(mapData);

                if (createComment is null) return ResultResponse<CommentDTO>.Failure(ResponseMessages.FailedCreation);

                return ResultResponse<CommentDTO>.Success(createComment.Data);
            }
        }
    }
}
