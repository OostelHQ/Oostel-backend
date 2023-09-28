using MapsterMapper;
using Microsoft.AspNetCore.SignalR;
using Oostel.Infrastructure.Repositories;

namespace Oostel.API.SIgnalR
{
    public class MessageHub : Hub
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        private readonly IHubContext<PresenceHub> _presenceHub;
        private readonly PresenceTracker _tracker;
    }
}
