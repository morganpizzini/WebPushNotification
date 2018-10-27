using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;

namespace WebPushNotif.Demo.Persistence
{
    public class SubscriptionsPersistence
    {
        public static IList<Lib.Net.Http.WebPush.PushSubscription> Subscriptions { get; set; } 
            = new List<Lib.Net.Http.WebPush.PushSubscription>();
    }
}
