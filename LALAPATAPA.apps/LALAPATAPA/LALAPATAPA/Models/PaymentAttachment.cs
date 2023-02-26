using System;
using System.Collections.Generic;

namespace LALAPATAPA.Models
{
    public partial class PaymentAttachment
    {
        public PaymentAttachment()
        {
            HeaderTransaction = new HashSet<HeaderTransaction>();
        }

        public int IdAttachment { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<HeaderTransaction> HeaderTransaction { get; set; }
    }
}
