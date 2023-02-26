using System;
using System.Collections.Generic;

namespace LALAPATAPA.services.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Account = new HashSet<Account>();
        }

        public int IdEmployee { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
