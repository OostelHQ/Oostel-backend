using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.UserMessage;
using Oostel.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public MessageRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }
        public void AddGroup(Group group)
        {
            _applicationDbContext.Groups.Add(group);
        }

        public void AddMessage(Message message)
        {
            _applicationDbContext.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            _applicationDbContext.Messages.Remove(message);
        }

        public async Task<Connection> GetConnection(string connectionId)
        {
            return await _applicationDbContext.Connections.FindAsync(connectionId);
        }

        public async Task<Group> GetGroupForConnection(string connectionId)
        {
            return await _applicationDbContext.Groups
                .Include(c => c.Connections)
                .Where(c => c.Connections.Any(x => x.ConnectionId == connectionId))
                .FirstOrDefaultAsync();
        }

        public async Task<Message> GetMessage(string id)
        {
           return await _applicationDbContext.Messages
                .Include(x => x.Sender)
                .Include(x => x.Recipient)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Group> GetMessageGroup(string groupName)
        {
            return await _applicationDbContext.Groups
                .Include(x => x.Connections)
                .FirstOrDefaultAsync(x => x.Name == groupName);
        }

        public async Task<PagedList<MessageDTO>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _applicationDbContext.Messages
                .OrderByDescending(m => m.MessengeSent)
                .Select(m => new MessageDTO()
                {
                    SenderEmail = m.SenderEmail,
                    SenderDeleted = m.SenderDeleted,
                    SenderId = m.SenderId,
                    Content = m.Content,
                    DateRead = m.DateRead,
                    MessageSent = m.MessengeSent,
                    RecipientDeleted = m.RecipientDeleted,
                    RecipientId = m.RecipientId,
                    RecipientEmail = m.RecipientEmail,
                    RecipientPhotoUrl = m.Recipient.ProfilePhotoURL,
                    SenderPhotoUrl = m.Sender.ProfilePhotoURL
                })
                .AsQueryable();

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.RecipientEmail == messageParams.Email
                    && u.RecipientDeleted == false),
                "Outbox" => query.Where(u => u.SenderEmail == messageParams.Email
                    && u.SenderDeleted == false),
                _ => query.Where(u => u.RecipientEmail ==
                    messageParams.Email && u.RecipientDeleted == false && u.DateRead == null)
            };

            return await PagedList<MessageDTO>.CreateAsync(query, messageParams.PageNumber, messageParams.PageSize);
        }

        public Task<IEnumerable<MessageDTO>> GetMessageThread(string currentUsername, string recipientUsername)
        {
            throw new NotImplementedException();
        }

        public void RemoveConnection(Connection connection)
        {
            throw new NotImplementedException();
        }
    }
}
