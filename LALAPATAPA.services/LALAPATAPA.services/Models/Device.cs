using System;
using System.Collections.Generic;

namespace LALAPATAPA.services.Models
{
    public partial class Device
    {
        public int IdDevice { get; set; }
        public int? IdAccount { get; set; }
        public string InstallationId { get; set; }

        public virtual Account IdAccountNavigation { get; set; }
    }
}
