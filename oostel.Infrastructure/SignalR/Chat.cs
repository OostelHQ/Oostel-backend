using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.SignalR;
    public record Chat(
        string message, 
        string receiverId, 
        string senderId, 
        string? mediaUrl, 
        DateTime timestamp);
    

