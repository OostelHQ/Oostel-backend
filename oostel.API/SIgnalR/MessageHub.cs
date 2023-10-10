using MapsterMapper;
using Microsoft.AspNetCore.SignalR;
using Oostel.API.Extensions;
using Oostel.Application.Modules.UserMessage.DTOs;
using Oostel.Application.Modules.UserProfiles.Services;
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
        private readonly IUserRolesProfilesService _rolesProfilesService;

        public MessageHub(IMapper mapper, UnitOfWork unitOfWork, IUserRolesProfilesService rolesProfilesService, IHubContext<PresenceHub> presenceHub, PresenceTracker tracker)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _presenceHub = presenceHub;
            _tracker = tracker;
            _rolesProfilesService = rolesProfilesService;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["user"].ToString();

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

            var sender = await _rolesProfilesService.GetLastnameAsync(username);
            var recipient = await  _rolesProfilesService.GetLastnameAsync(createMessageDto.RecipientLastname);

            if (recipient == null) throw new HubException("Not found user");

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderLastName = sender.UserName,
                RecipientLastName = recipient.UserName,
                Content = createMessageDto.Content
            };


                var connections = await _tracker.GetConnectionsForUser(recipient.UserName);
                if (connections != null)
                {
                    await _presenceHub.Clients.Clients(connections).SendAsync("NewMessageReceived",
                        new { username = sender.LastName, knownAs = sender.LastName });
                }
       

            _unitOfWork.MessageRepository.AddMessage(message);

        }
    }
}
