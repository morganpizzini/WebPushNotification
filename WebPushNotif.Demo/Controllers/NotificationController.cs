using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;
using Microsoft.AspNetCore.Mvc;
using WebPushNotif.Demo.Interfaces;
using WebPushNotif.Demo.Models;

namespace WebPushNotif.Demo.Controllers
{
    public class NotificationController : Controller
    {
        private readonly IPushNotificationsQueue _pushNotificationsQueue;

        public NotificationController(IPushNotificationsQueue pushNotificationsQueue)
        {
            _pushNotificationsQueue = pushNotificationsQueue;
        }

        /// <summary>
        /// invio una notifica a tutti i sottoscrittori
        /// </summary>
        /// <returns>IActionResult</returns>
        public IActionResult SendNotification()
        {
            //creo il corpo della notifica
            var notificationBody = "{\"notification\":{\"title\":\"Angular News\",\"body\":\"Newsletter Available!\",\"icon\":\"assets/icons/icon-96x96.png\",\"vibrate\":[100,50,100],\"data\":{\"dateOfArrival\":1537101991875,\"primaryKey\":1,\"url\":\"http://google.com\"},\"actions\":[{\"action\":\"explore\",\"title\":\"Go to the site\"},{\"action\":\"second\",\"title\":\"Second action\",\"icon\":\"assets/icons/icon-72x72.png\"}]}}";

            var notification = new PushMessage(notificationBody)
            {
                Topic = "Message Topic",
                Urgency = PushMessageUrgency.Low,
            };

            //ciclo su tutti i sottoscrittori
            foreach (var subscription in Persistence.SubscriptionsPersistence.Subscriptions)
            {
                //inserisco la notifica e il sottoscrittore nella coda di invio
                _pushNotificationsQueue.Enqueue(
                    new PushMessageContract(subscription,
                        notification));
            }
            return Ok(true);
        }

    }
}
