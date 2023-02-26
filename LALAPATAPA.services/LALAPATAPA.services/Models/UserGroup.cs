using System;
using System.Collections.Generic;

namespace LALAPATAPA.services.Models
{
    public partial class UserGroup
    {
        public UserGroup()
        {
            Account = new HashSet<Account>();
        }

        public int IdGroup { get; set; }
        public string GroupName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
