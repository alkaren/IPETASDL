using System;
using System.Collections.Generic;

namespace LALAPATAPA.Models
{
    public partial class BankAccount
    {
        public BankAccount()
        {
            DetailTransaction = new HashSet<DetailTransaction>();
        }

        public int IdBankAccount { get; set; }
        public string AccountNo { get; set; }
        public string AccountOwner { get; set; }
        public string BankName { get; set; }
        public DateTime? ValidStart { get; set; }
        public DateTime? ValidUntil { get; set; }

        public virtual ICollection<DetailTransaction> DetailTransaction { get; set; }
    }
}
