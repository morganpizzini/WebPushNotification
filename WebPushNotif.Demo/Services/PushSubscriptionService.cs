using System;
using System.Threading;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;
using WebPushNotif.Demo.Interfaces;

namespace WebPushNotif.Demo.Services
{
    /// <summary>
    /// Service layer for retrive pushsubscription from store / persistence
    /// </summary>
    //public class PushSubscriptionService : IPushSubscriptionStore
    //{
    //    public Task ForEachSubscriptionAsync(Action<PushSubscription> action, CancellationToken cancellationToken)
    //    {
    //        //recupero la lista delle sottoscrizioni
    //        foreach (var pushSubscription in Persistence.SubscriptionsPersistence.Subscriptions)
    //        {
    //            //esegui l'azione
    //            action(pushSubscription);
    //        }
    //        return Task.FromResult(true);
    //    }
    //}
}
