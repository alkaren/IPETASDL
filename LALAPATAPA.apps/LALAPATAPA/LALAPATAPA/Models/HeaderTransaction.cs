using System;
using System.Collections.Generic;

namespace LALAPATAPA.Models
{
    public partial class HeaderTransaction
    {
        public HeaderTransaction()
        {
            DetailTransaction = new HashSet<DetailTransaction>();
            TransactionLog = new HashSet<TransactionLog>();
        }

        public int IdTransaction { get; set; }
        public int? IdCustomer { get; set; }
        public int? IdAttachment { get; set; }
        public string TransactionNumber { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string TransactionStatus { get; set; }
        public string PaymentMethode { get; set; }
        public decimal? PaymentTotal { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string OrderAttachmentType { get; set; }
        public string OrderAttachmentUrl { get; set; }
        public string RequestDeliveryStatus { get; set; }
        public string CustomerConfirmation { get; set; }

        public virtual PaymentAttachment IdAttachmentNavigation { get; set; }
        public virtual Customer IdCustomerNavigation { get; set; }
        public virtual ICollection<DetailTransaction> DetailTransaction { get; set; }
        public virtual ICollection<TransactionLog> TransactionLog { get; set; }
    }
}
