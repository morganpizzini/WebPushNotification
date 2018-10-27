using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;

namespace WebPushNotif.Demo.Interfaces
{
    public interface IPushNotificationService
    {
        Task SendNotificationAsync(PushSubscription subscription, PushMessage message);

        Task SendNotificationAsync(PushSubscription subscription, PushMessage message, CancellationToken cancellationToken);
    }
}
