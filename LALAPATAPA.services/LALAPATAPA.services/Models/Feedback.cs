using System;
using System.Collections.Generic;

namespace LALAPATAPA.services.Models
{
    public partial class Feedback
    {
        public int IdFeedback { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public DateTime? Time { get; set; }
        public int? IdAccount { get; set; }

        public virtual Account IdAccountNavigation { get; set; }
    }
}
