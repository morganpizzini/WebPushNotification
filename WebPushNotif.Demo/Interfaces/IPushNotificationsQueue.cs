using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;
using WebPushNotif.Demo.Models;

namespace WebPushNotif.Demo.Interfaces
{
    public interface IPushNotificationsQueue
    {
        void Enqueue(PushMessageContract message);

        Task<PushMessageContract> DequeueAsync(CancellationToken cancellationToken);
    }
}
