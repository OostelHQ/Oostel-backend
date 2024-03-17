using Microsoft.AspNetCore.Http;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.UserMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using userMessage = Oostel.Domain.UserMessage.UserMessage;

namespace Oostel.Infrastructure.SignalR.Services
{
    public interface IMessagingService 
    {
        Task<userMessage> CreateChat(userMessage createChatDTO);
        Task<IEnumerable<userMessage>> GetAllChatHistory(string userId, ChatParam pagingParams);
        Task<PagedList<userMessage>> GetMyChatWithSomeone(string userId, string receiverId, ChatParam pagingParams);
        Task<bool> DeleteChatHistory(string userId, string receiverId);
        Task<bool> DeleteMyChatWithSomeone(string messageId, string userId, string receiverId);
        Task<PagedList<string>> GetAllReceivers(string userId, ChatParam pagingParams);
        Task<UserMessage> SendMediaFile(IFormFile file, string userId, string recipientId);
    }
}
