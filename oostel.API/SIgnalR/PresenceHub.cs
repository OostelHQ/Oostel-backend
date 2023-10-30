using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Oostel.Application.UserAccessors;
using System.Security.Claims;
using Oostel.API.Extensions;

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
            var isOnline = await _tracker.UserConnected(Context.User.GetUserEmail(), Context.ConnectionId);
            if (isOnline)
                await Clients.Others.SendAsync("UserIsOnline", Context.User.GetUserEmail());

            var currentUsers = await _tracker.GetOnlineUsers();
            await Clients.Caller.SendAsync("GetOnlineUsers", currentUsers);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var isOffline = await _tracker.UserDisconnected(Context.User.GetUserEmail(), Context.ConnectionId);

            if (isOffline)
                await Clients.Others.SendAsync("UserIsOffline", Context.User.GetUserEmail());

            await base.OnDisconnectedAsync(exception);
        }

    
    }
}
