using MapsterMapper;
using Microsoft.AspNetCore.SignalR;
using Oostel.API.Extensions;
using Oostel.Application.Modules.UserMessage.DTOs;
using Oostel.Common.Types;
using Oostel.Domain.UserMessage;
using Oostel.Infrastructure.Repositories;

namespace Oostel.API.SIgnalR
{
    public class MessageHub : Hub
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        private readonly IHubContext<PresenceHub> _presenceHub;
        private readonly PresenceTracker _tracker;

        public MessageHub(IMapper mapper, UnitOfWork unitOfWork, IHubContext<PresenceHub> presenceHub, PresenceTracker tracker)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _presenceHub = presenceHub;
            _tracker = tracker;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["user"].ToString();
           // var groupName = GetGroupName(Context.User.GetUsername(), otherUser);
           // await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
           // var group = await AddToGroup(groupName);
          //  await Clients.Group(groupName).SendAsync("UpdatedGroup", group);

            var messages = await _unitOfWork.MessageRepository.
                GetMessageThread(Context.User.GetUsername(), otherUser);

            if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

            await Clients.Caller.SendAsync("ReceiveMessageThread", messages);
        }

        public async Task SendMessage(CreateMessageDTO createMessageDto)
        {
            var username = Context.User.GetUsername();

            if (username == createMessageDto.RecipientLastname.ToLower())
                throw new HubException("You cannot send messages to yourself");

            var sender = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
            var recipient = await _unitOfWork.UserRepository.GetUserByUsernameAsync(createMessageDto.RecipientLastname);

            if (recipient == null) throw new HubException("Not found user");

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderLastName = sender.UserName,
                RecipientLastName = recipient.UserName,
                Content = createMessageDto.Content
            };

            //var groupName = GetGroupName(sender.UserName, recipient.UserName);

            //var group = await _unitOfWork.MessageRepository.GetMessageGroup(groupName);

            //if (group.Connections.Any(x => x.Username == recipient.UserName))
            //{
            //    message.DateRead = DateTime.UtcNow;
            //}
            //else
            //{
                var connections = await _tracker.GetConnectionsForUser(recipient.UserName);
                if (connections != null)
                {
                    await _presenceHub.Clients.Clients(connections).SendAsync("NewMessageReceived",
                        new { username = sender.UserName, knownAs = sender.KnownAs });
                }
       

            _unitOfWork.MessageRepository.AddMessage(message);

        }
    }
}
