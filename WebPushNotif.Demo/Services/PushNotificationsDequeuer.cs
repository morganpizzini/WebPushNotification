using System;
using System.Threading;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebPushNotif.Demo.Interfaces;
using WebPushNotif.Demo.Models;

namespace WebPushNotif.Demo.Services
{
    /// <summary>
    /// IHostedServise for dequeue message and send notifications
    /// </summary>
    public class PushNotificationsDequeuer : IHostedService
    {
        private readonly IPushNotificationsQueue _messagesQueue;
        private readonly IPushNotificationService _notificationService;
        //private readonly IServiceProvider _serviceProvider;
        private readonly CancellationTokenSource _stopTokenSource = new CancellationTokenSource();

        private Task _dequeueMessagesTask;

        public PushNotificationsDequeuer(IPushNotificationsQueue messagesQueue, IPushNotificationService notificationService,
            )//IServiceProvider serviceProvider)
        {
            _messagesQueue = messagesQueue;
            _notificationService = notificationService;
            //_serviceProvider = serviceProvider;
        }
        /// <summary>
        /// operazioni all'avvio del processo
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // inizializzo il metodo per recuperare gli elementi della coda
            _dequeueMessagesTask = Task.Run(DequeueMessagesAsync);

            return Task.CompletedTask;
        }

        /// <summary>
        /// operazioni al termine del processo
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            //blocco il token
            _stopTokenSource.Cancel();

            return Task.WhenAny(_dequeueMessagesTask, Task.Delay(Timeout.Infinite, cancellationToken));
        }

        /// <summary>
        /// invio delle notifiche
        /// </summary>
        /// <returns></returns>
        private async Task DequeueMessagesAsync()
        {
            while (!_stopTokenSource.IsCancellationRequested)
            {
                //recupero del primo messaggio dalla coda
                PushMessageContract messageContract = await _messagesQueue.DequeueAsync(_stopTokenSource.Token);

                if (!_stopTokenSource.IsCancellationRequested)
                {
                    //invio notifica
                    await _notificationService.SendNotificationAsync(messageContract.Subscription,messageContract.NotificationMessage, _stopTokenSource.Token);

                }
            }
        }

    }
}
