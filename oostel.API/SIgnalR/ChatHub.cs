using MediatR;
using Microsoft.AspNetCore.SignalR;
using Oostel.Application.Modules.Hostel.Features.Commands;

namespace Oostel.API.SIgnalR
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator) => _mediator= mediator;

        public async Task SendComment(CreateCommentCommand command)
        {
            var comment = await _mediator.Send(command);

            await Clients.Groups(command.HostelId)
                .SendAsync("ReceiveComment", comment.Data);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            var hostelId = httpContext.Request.Query["hostelId"];
            await Groups.AddToGroupAsync(Context.ConnectionId, hostelId);

            var result = await _mediator.Send(new List.)
        }
    }
}
