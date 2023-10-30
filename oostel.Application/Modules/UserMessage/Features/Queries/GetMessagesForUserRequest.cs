using Oostel.Application.Modules.UserMessage.DTOs;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Application.Modules.UserProfiles.Features.Queries;
using Oostel.Application.Modules.UserProfiles.Services;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserMessage.Features.Queries
{
    public class GetMessagesForUserRequest: IRequest<ResultResponse<IQueryable<MessageDTO>>>
    {
        public MessageParams MessageParams { get; set; }

        public sealed class GetMessagesForUserRequestCommand : IRequestHandler<GetMessagesForUserRequest, ResultResponse<IQueryable<MessageDTO>>>
        {
            private readonly UnitOfWork _unitOfWork;
            public GetMessagesForUserRequestCommand(UnitOfWork unitOfWork) =>
                _unitOfWork = unitOfWork;

            public async Task<ResultResponse<IQueryable<MessageDTO>>> Handle(GetMessagesForUserRequest request, CancellationToken cancellationToken)
            {
                var getMessages = await _unitOfWork.MessageRepository.GetMessagesForUser(request.MessageParams);

                if (getMessages is null) return ResultResponse<IQueryable<MessageDTO>>.Failure(ResponseMessages.NotFound);

               ResultResponse<IQueryable<MessageDTO>>.Success(await PagedList<CreateMessageDTO>.CreateAsync(request.MessageParams.PageNumber, request.MessageParams.PageSize));
            }
        }
    }
}
