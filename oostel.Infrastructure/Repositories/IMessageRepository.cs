using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.UserMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.Repositories
{
    public interface IMessageRepository
    {
        void AddGroup(Group group);

        void RemoveConnection(Connection connection);

        Task<Connection> GetConnection(string connectionId);

        Task<Group> GetMessageGroup(string groupName);

        Task<Group> GetGroupForConnection(string connectionId);

        void AddMessage(Message message);

        void DeleteMessage(Message message);

        Task<Message> GetMessage(string id);

        Task<PagedList<MessageDTO>> GetMessagesForUser(MessageParams messageParams);

        Task<IEnumerable<MessageDTO>> GetMessageThread(string currentUsername, string recipientUsername);
    }
}
