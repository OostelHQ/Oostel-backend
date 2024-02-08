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
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserMessage.Features.Queries
{
    public class DeleteMessageRequest: IRequest<APIResponse>
    {
        public string MessageId { get; set; }
        public string Email { get; set; }

        public sealed class DeleteMessageRequestCommand : IRequestHandler<DeleteMessageRequest, APIResponse>
        {
            private readonly UnitOfWork _unitOfWork;
            public DeleteMessageRequestCommand(UnitOfWork unitOfWork) =>
                _unitOfWork = unitOfWork;

            public async Task<APIResponse> Handle(DeleteMessageRequest request, CancellationToken cancellationToken)
            {
                var getMessage = await _unitOfWork.MessageRepository.GetMessage(request.MessageId);

                if(getMessage.Sender.Email != request.Email && getMessage.Recipient.Email != request.Email)
                    return APIResponse.GetFailureMessage(HttpStatusCode.Unauthorized, null, ResponseMessages.UnAuthorizedMessage);

                if (getMessage.Sender.Email == request.Email)
                    getMessage.SenderDeleted = true;

                if (getMessage.Recipient.Email == request.Email)
                    getMessage.RecipientDeleted = true;

                if (getMessage.SenderDeleted && getMessage.RecipientDeleted)
                    _unitOfWork.MessageRepository.DeleteMessage(getMessage);

                if(await _unitOfWork.Complete())
                    return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: null , ResponseMessages.DeleteMessage);

                return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedToDeleteMessage);

            }
        }
    }
}
