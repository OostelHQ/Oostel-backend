using Microsoft.AspNetCore.SignalR;
using Oostel.Application.UserAccessors;

namespace Oostel.API.SIgnalR
{
    public class PresenceHub : Hub
    {
        private readonly PresenceTracker _tracker;
        private readonly IUserAccessor _userAccessor;

        public PresenceHub(PresenceTracker tracker, IUserAccessor userAccessor)
        {
            _tracker = tracker;
            _userAccessor = userAccessor;
        }

        public override async Task OnConnectedAsync()
        {
            var isOnline = await _tracker.UserConnected(Context.User., Context.ConnectionId);
            if (isOnline)
                await Clients.Others.SendAsync("UserIsOnline", Context.User.GetUsername());

            var currentUsers = await _tracker.GetOnlineUsers();
            await Clients.Caller.SendAsync("GetOnlineUsers", currentUsers);
        }
    }
}
