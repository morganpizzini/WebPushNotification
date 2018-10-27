using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;
using WebPushNotif.Demo.Interfaces;
using WebPushNotif.Demo.Models;

namespace WebPushNotif.Demo.Services
{
    /// <summary>
    /// Enqueue and Dequeue messages operations
    /// </summary>
    public class PushNotificationsQueue : IPushNotificationsQueue
    {
        private readonly ConcurrentQueue<PushMessageContract> _messages = new ConcurrentQueue<PushMessageContract>();
        private readonly SemaphoreSlim _messageEnqueuedSignal = new SemaphoreSlim(0);

        public void Enqueue(PushMessageContract message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            _messages.Enqueue(message);

            _messageEnqueuedSignal.Release();
        }

        public async Task<PushMessageContract> DequeueAsync(CancellationToken cancellationToken)
        {
            await _messageEnqueuedSignal.WaitAsync(cancellationToken);

            _messages.TryDequeue(out PushMessageContract message);

            return message;
        }
    }
}
