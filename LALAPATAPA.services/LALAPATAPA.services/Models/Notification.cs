using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LALAPATAPA.services.Models
{
    public partial class Notification
    {
        public int Seq { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }

        //public NotificationType Type { get; set; }
    }
}
