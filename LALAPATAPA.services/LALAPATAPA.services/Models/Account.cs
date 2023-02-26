using System;
using System.Collections.Generic;

namespace LALAPATAPA.services.Models
{
    public partial class Account
    {
        public Account()
        {
            Device = new HashSet<Device>();
            Feedback = new HashSet<Feedback>();
        }

        public int IdAccount { get; set; }
        public int? IdGroup { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public int? IdCustomer { get; set; }
        public int? IdEmployee { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; }
        public virtual Employee IdEmployeeNavigation { get; set; }
        public virtual UserGroup IdGroupNavigation { get; set; }
        public virtual ICollection<Device> Device { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
    }
}
