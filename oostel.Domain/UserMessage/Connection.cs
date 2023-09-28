using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserMessage
{
    public class Connection
    {
        public string ConnectionId { get; set; }
        public string LastName { get; set; }

        public Connection()
        {

        }

        public Connection(string connectionId, string lastname)
        {
            ConnectionId = connectionId;
            LastName = lastname;
        }
    }
}
