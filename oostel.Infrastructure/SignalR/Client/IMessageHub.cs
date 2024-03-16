using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.SignalR.Client
{
    public interface IMessageHub
    {
        Task ReceiveMessage(Chat chat);
        Task ReceiveGroupMessage(Chat chat);
        Task SayWhoIsTyping(Chat chat);
    }
}
