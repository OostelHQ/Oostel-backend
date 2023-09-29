using Microsoft.AspNetCore.SignalR;
using Oostel.Application.UserAccessors;
using System.Security.Claims;

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

       
    }
}
