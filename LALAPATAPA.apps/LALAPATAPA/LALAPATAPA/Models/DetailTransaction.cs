using System;
using System.Collections.Generic;

namespace LALAPATAPA.Models
{
    public partial class DetailTransaction
    {
        public int IdDetail { get; set; }
        public int? IdTransaction { get; set; }
        public int? IdProduct { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? Discount { get; set; }
        public string TransactionDescription { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public int? IdBankAccount { get; set; }
        public string ProcurementPurpose { get; set; }

        public virtual BankAccount IdBankAccountNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
        public virtual HeaderTransaction IdTransactionNavigation { get; set; }
    }
}
