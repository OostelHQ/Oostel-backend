using Microsoft.AspNetCore.SignalR;

namespace Oostel.API.SIgnalR
{
    public class PresenceHub : Hub
    {
        private readonly PresenceTracker _tracker;

        public PresenceHub()
        {

        }
    }
}
