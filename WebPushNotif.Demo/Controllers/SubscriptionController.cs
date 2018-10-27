using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPushNotif.Demo.Persistence;

namespace WebPushNotif.Demo.Controllers
{
    public class SubscriptionController : Controller
    {
        /// <summary>
        /// sottoscrizione client
        /// </summary>
        /// <param name="subscription">oggetto sottoscrizione fornito dal client</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult SubscribeClient(Lib.Net.Http.WebPush.PushSubscription subscription)
        {
            //validazione argomenti
            if (subscription == null)
                return BadRequest($"{nameof(subscription)} cannot be null");

            //aggiunta sottoscrizione a registro applicazione
            SubscriptionsPersistence.Subscriptions.Add(subscription);

            //risultato operazione
            return Ok(true);
        }

        /// <summary>
        /// rimozione sottoscrizione client
        /// </summary>
        /// <param name="endPoint">client endpoint</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult UnSubscribeClient(string endPoint)
        {
            //validazione argomenti
            if (endPoint == null)
                return BadRequest($"{nameof(endPoint)} cannot be null");

            //recupero la sottoscrizione
            var subscription = SubscriptionsPersistence.Subscriptions.SingleOrDefault(x => x.Endpoint == endPoint);

            if (subscription == null)
            {
                return BadRequest($"Subscription with provided {nameof(endPoint)} not found");
            }
            //rimuovo la sottoscrizione
            SubscriptionsPersistence.Subscriptions.Remove(subscription);

            //risultato operazione
            return Ok(true);
        }
    }
}
