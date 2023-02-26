using System;
using System.Collections.Generic;

namespace LALAPATAPA.services.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Account = new HashSet<Account>();
            HeaderTransaction = new HashSet<HeaderTransaction>();
        }

        public int IdCustomer { get; set; }
        public string Name { get; set; }
        public string IdentityNumber { get; set; }
        public string Company { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<HeaderTransaction> HeaderTransaction { get; set; }
    }
}
