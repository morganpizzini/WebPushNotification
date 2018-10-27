using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using WebPushNotif.Demo.Interfaces;

namespace WebPushNotif.Demo.Services
{
    /// <summary>
    /// Service che all'inizializzazione crea il push client e espone i metodi per invio notifiche
    /// </summary>
    public class NotificationService : IPushNotificationService
    {
        private readonly PushServiceClient _pushClient;

        public NotificationService(PushServiceClient pushClient, IVapidTokenCache tokenCache) // ), ILogger<PushServicePushNotificationService> logger)
        {
            var _options = new
            {
                Subject = "https://youdomain.com",
                PublicKey = "YOUR_PUBLIC_KEY",
                PrivateKey = "YOUR_PRIVATE_KEY"
            };

            _pushClient = pushClient;
            _pushClient.DefaultAuthentication = new VapidAuthentication(_options.PublicKey, _options.PrivateKey)
            {
                Subject = _options.Subject,
                TokenCache = tokenCache
            };
        }

        public Task SendNotificationAsync(PushSubscription subscription, PushMessage message)
        {
            return SendNotificationAsync(subscription, message, CancellationToken.None);
        }

        public async Task SendNotificationAsync(PushSubscription subscription, PushMessage message, CancellationToken cancellationToken)
        {
            try
            {
                await _pushClient.RequestPushMessageDeliveryAsync(subscription, message, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
