using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.APIRoutes;
using Oostel.API.ViewModels.HostelsVM;
using Oostel.API.ViewModels.MessageVM;
using Oostel.Application.Modules.Hostel.Features.Commands;
using Oostel.Application.Modules.UserMessage.Features.Commands;
using Oostel.Application.Modules.UserMessage.Features.Queries;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;

namespace Oostel.API.Controllers
{
    public class MessageController : BaseController
    {
        private readonly IMapper _mapper;
        public MessageController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        [Route(MessageRoute.SendMessage)]
        public async Task<ActionResult<APIResponse>> SendMessage([FromBody]SendMessageRequest request)
        {
            var mapData = _mapper.Map<SendMessageCommand>(request);
            return HandleResult(await Mediator.Send(mapData));
        }

        [HttpGet]
        [Route(MessageRoute.GetAllChatHistory)]
        public async Task<ActionResult<APIResponse>> GetAllChatHistory([FromQuery] string senderId, [FromQuery] ChatParam chatParam)
        {
            return HandleResult(await Mediator.Send(new GetAllChatHistoryQuery 
            { SenderId = senderId, ChatParam = chatParam })) ;
        }

        [HttpGet]
        [Route(MessageRoute.GetAllReceivers)]
        public async Task<ActionResult<APIResponse>> GetAllReceivers([FromQuery] string senderId, [FromQuery]ChatParam chatParam)
        {
            return HandleResult(await Mediator.Send(new GetAllReceiversQuery
            { SenderId = senderId, ChatParam = chatParam }));
        }

        [HttpGet]
        [Route(MessageRoute.GetMyChatWithSomeone)]
        public async Task<ActionResult<APIResponse>> GetMyChatWithSomeone([FromQuery] GetMyChatWithSomeoneRequest request)
        {
            return HandleResult(await Mediator.Send(new GetMyChatWithSomeoneQuery
            { SenderId = request.SenderId, ReceiverId = request.ReceiverId, ChatParam = request.ChatParam }));
        }

        [HttpDelete]
        [Route(MessageRoute.DeleteMyMessageWithSomeone)]
        public async Task<ActionResult<APIResponse>> DeleteMyMessageWithSomeone([FromBody] DeleteMyChatWithSomeoneRequest request)
        {
            return HandleResult(await Mediator.Send(new DeleteMyMessageWithSomeoneCommand
            { Id = request.Id, SenderId = request.SenderId, ReceiverId = request.ReceiverId}));
        }

        [HttpDelete]
        [Route(MessageRoute.DeleteChatHistory)]
        public async Task<ActionResult<APIResponse>> DeleteChatHistory([FromBody] string senderId, string receiverId)
        {
            return HandleResult(await Mediator.Send(new DeleteChatHistoryCommand
            { SenderId = senderId, ReceiverId = receiverId}));
        }

        [HttpPut]
        [Route(MessageRoute.UploadPictureChat)]
        public async Task<ActionResult<APIResponse>> UploadPictureChat([FromBody] UploadChatPictureRequest request)
        {
            return HandleResult(await Mediator.Send(new UploadPictureChatCommand
            { SenderId = request.SenderId, ReceiverId = request.ReceiverId, Message = request.Message, MediaFile = request.MediaFile }));
        }
    }
}
