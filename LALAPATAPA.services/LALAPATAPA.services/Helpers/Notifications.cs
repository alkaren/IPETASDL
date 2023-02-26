using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;

namespace LALAPATAPA.services.Helpers
{
    public class Notifications
    {
        public static Notifications Instance = new Notifications();

        public NotificationHubClient Hub { get; set; }

        private Notifications()
        {
            Hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://lalapatapa.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=82Bu3C0tADYXgIGv9KK3/fLQ6pOj+SumislzxIU+CBA=",
                                                                            "lalapatapanotificationhub");
        }
    }
}
