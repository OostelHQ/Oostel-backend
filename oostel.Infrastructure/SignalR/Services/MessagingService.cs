using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.UserMessage;
using Oostel.Infrastructure.Data;
using Oostel.Infrastructure.Media;
using Oostel.Infrastructure.Repositories;
using Oostel.Infrastructure.SignalR.Client;

namespace Oostel.Infrastructure.SignalR.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMediaUpload _mediaUploader;
        private readonly ApplicationDbContext _dbContext;
        private readonly IHubContext<MessageHub, IMessageHub> _hubContext;
        public MessagingService(UnitOfWork unitOfWork, IMediaUpload mediaUpload, ApplicationDbContext dbContext)
        {
           _unitOfWork = unitOfWork;
           _mediaUploader = mediaUpload;
           _dbContext= dbContext;
        }
        public async Task<UserMessage> CreateChat(UserMessage createChatDTO)
        {
            var createChat = await _unitOfWork.UserMessageRepository.Add(createChatDTO);
            await _unitOfWork.SaveAsync();

            return createChat;
        }

        public async Task<bool> DeleteMyChatWithSomeone(string messageId, string userId, string receiverId)
        {
            var getUserChatWithSomeone = await _unitOfWork.UserMessageRepository.Find(x => x.Id == messageId && x.SenderId == userId && x.ReceiverId == receiverId);

            var deleteChat = await _unitOfWork.UserMessageRepository.Remove(getUserChatWithSomeone);
            await _unitOfWork.SaveAsync();

            return true;          
        }

        public async Task<bool> DeleteChatHistory(string userId, string receiverId)
        {
            var getChatHistory = _unitOfWork.UserMessageRepository.FindByCondition(x => x.SenderId == userId && x.ReceiverId == receiverId, false);
            foreach (var chat in getChatHistory)
            {
                await _unitOfWork.UserMessageRepository.Remove(chat);
                await _unitOfWork.SaveAsync();
            }

            return true;
        }

        public async Task<IEnumerable<UserMessage>> GetAllChatHistory(string userId, ChatParam pagingParams)
        {
            var chats = _dbContext.UserMessages
                           .OrderByDescending(x => x.Timestamp)
                           .Where(x => x.SenderId == userId)
                           .AsNoTracking()
                           .AsQueryable();

            return await PagedList<UserMessage>.CreateAsync(chats, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<IEnumerable<UserMessage>> GetAllReceiversWithLastChat(string userId)
        {
            // Group messages by receiver and get the latest message for each group
            var messages = await _dbContext.UserMessages
                .Where(x => x.SenderId == userId)
                .GroupBy(x => x.ReceiverId)
                .Select(group => group.OrderByDescending(m => m.Timestamp).FirstOrDefault())
                .ToListAsync();

            // Optionally, include user information from User table (assuming a User model)
            var userMessages = messages.Select(message =>
            {
                var userMessage = new UserMessage // Consider using a dedicated model class
                {
                    ReceiverId = message.ReceiverId,
                    Message = message.Message,
                    Timestamp = message.Timestamp,
                    SenderId = message.SenderId,
                    CreatedDate = message.CreatedDate,
                    LastModifiedDate = message.LastModifiedDate,
                    Id = message.Id,
                    MediaUrl = message.MediaUrl
                };
                // Add user information if needed (assuming a separate User model)
                // userMessage.User = _dbContext.Users.FirstOrDefault(u => u.Id == message.ReceiverId);
                return userMessage;
            });

            return userMessages;
        }


        public async Task<PagedList<string>> GetAllReceivers(string userId, ChatParam pagingParams)
        {
            var chats = _dbContext.UserMessages
                           .Where(x => x.SenderId == userId)
                           .Select(x => x.ReceiverId)
                           .Distinct()
                           .AsNoTracking()
                           .AsQueryable();

            return await PagedList<string>.CreateAsync(chats, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<PagedList<UserMessage>> GetMyChatWithSomeone(string userId, string receiverId, ChatParam pagingParams)
        {
            var chats = _dbContext.UserMessages
                            .OrderByDescending(x => x.Timestamp)
                            .Where(x => x.SenderId == userId && x.ReceiverId == receiverId)
                            .AsNoTracking()
                            .AsQueryable();

            return await PagedList<UserMessage>.CreateAsync(chats, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<UserMessage> SendMediaFile(IFormFile file, string userId, string recipientId)
        {
            var photoResult = _mediaUploader.UploadPhoto(file);
            var chat = new Chat(string.Empty, recipientId, userId, photoResult.Result.Url, DateTime.Now);
            await _hubContext.Clients.User(chat.receiverId).ReceiveMessage(chat);
            var chatHistory = new UserMessage(Guid.NewGuid().ToString(), string.Empty, recipientId, userId, photoResult.Result.Url);
            return await CreateChat(chatHistory);
        }
    }
}
