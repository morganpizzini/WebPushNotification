using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;

namespace WebPushNotif.Demo.Models
{
    /// <summary>
    /// contratto per web push notification
    /// </summary>
    public class PushMessageContract
    {
        public PushMessageContract(PushSubscription subscription,
            PushMessage notificationMessage)
        {
            Subscription = subscription;
            NotificationMessage = notificationMessage;
        }

        /// <summary>
        /// sottoscrizione target
        /// </summary>
        public PushSubscription Subscription { get; set; }

        /// <summary>
        /// notifica push
        /// </summary>
        public PushMessage NotificationMessage { get; set; }
    }
}
