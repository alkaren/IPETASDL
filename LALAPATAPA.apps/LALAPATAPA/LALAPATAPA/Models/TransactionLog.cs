using System;
using System.Collections.Generic;

namespace LALAPATAPA.Models
{
    public partial class TransactionLog
    {
        public int IdTransactioLog { get; set; }
        public int? IdTransaction { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public DateTime? Time { get; set; }
        public string Target { get; set; }

        public virtual HeaderTransaction IdTransactionNavigation { get; set; }
    }
}
