using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserAuthentication.Events
{
    public class ReferreeAddedEventPublisher
    {
        public delegate Task MyEventHandler(ReferreeAddedEventArgs args);
        public static event MyEventHandler ReferreeAdded;

        // Publisher : Method that raises the Event
        public static Task OnReferreeAdded(ReferreeAddedEventArgs e)
        {
            MyEventHandler referreeAdded = ReferreeAdded;
            if (referreeAdded != null)
            {
                referreeAdded?.Invoke(e);
            }

            return Task.CompletedTask;
        }

    }

    public class ReferreeAddedEventArgs : EventArgs
    {
        public string UserId { get; set; }
        public ReferreeAddedEventArgs(string userId)
        {
            userId = userId;
        }
    }
}
