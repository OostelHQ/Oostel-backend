using Microsoft.AspNetCore.SignalR;
using System;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.SignalR
{
    public class GenericRepositoryHub<TClient> : Hub<TClient> where TClient : class
    {
        public static ConcurrentDictionary<string?, List<string>>? ConnectedUsers = new ConcurrentDictionary<string?, List<string>>();

        public GenericRepositoryHub()
        {

        }

        public override async Task OnConnectedAsync()
        {
            string? userid = Context.User?.Identity?.Name;
            if (userid == null || userid.Equals(string.Empty))
            {
                Trace.TraceInformation("User did not log in; could not connect to signalr service");
                return;
            }

            Trace.TraceInformation(userid + "connected");

            List<string>? existUserConnectionIds;
            ConnectedUsers.TryGetValue(userid, out existUserConnectionIds);
            existUserConnectionIds ??= new List<string>();
            existUserConnectionIds.Add(Context.ConnectionId);
            ConnectedUsers.TryAdd($"{userid}-{Context.UserIdentifier}", existUserConnectionIds);
            await base.OnConnectedAsync();

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string? userid = Context.User?.Identity?.Name;
            if (userid == null || userid.Equals(string.Empty))
            {
                Trace.TraceInformation("User did not log in; could not connect to signalr service");
                return;
            }
            if (ConnectedUsers.ContainsKey($"{userid}-{Context.UserIdentifier}"))
            {
                ConnectedUsers.TryRemove($"{userid}-{Context.UserIdentifier}", out List<string>? existUserConnectionIds);
                existUserConnectionIds.Remove(Context.ConnectionId);
                ConnectedUsers.TryAdd($"{userid}-{Context.UserIdentifier}", existUserConnectionIds);

            }
            await base.OnDisconnectedAsync(exception);
        }

    }
}
