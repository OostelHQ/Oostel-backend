using Mailjet.Client.Resources;
using MapsterMapper;
using Oostel.Domain.UserMessage;
using Oostel.Infrastructure.SignalR.Client;
using Oostel.Infrastructure.SignalR.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.SignalR
{
    public class MessageHub : GenericRepositoryHub<IMessageHub>
    {
        private readonly IMessagingService _messagingService;
        private readonly IMapper _mapper;

        public MessageHub(IMessagingService messagingService, IMapper mapper)
        {
            _messagingService= messagingService;
            _mapper= mapper;
        }

        public async Task SendMessageToUser(Chat chat)
        {
            var mapChat = new UserMessage()
            {
                Message = chat.message,
                MediaUrl = chat.mediaUrl,
                ReceiverId = chat.receiverId,
                SenderId = chat.senderId,
                Timestamp = chat.timestamp,
            };

            var result = await _messagingService.CreateChat(mapChat);
            await Clients.User(result.ReceiverId).ReceiveMessage(chat);
        }

        public async Task<List<string>> GetConnectedUser()
        {

            List<string?> data = ConnectedUsers.Keys.ToList();
            return data;
        }

        public async Task UserIsTyping(Chat chat)
        {
            await Clients.User(chat.receiverId).SayWhoIsTyping(chat);
        }


    }
}
