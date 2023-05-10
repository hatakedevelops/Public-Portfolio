using System;
using System.Collections.Generic;

namespace RegistrationService.Data.Entities
{
    public partial class Account
    {
        public Account()
        {
            TransferAccountReceivedFkNavigations = new HashSet<Transfer>();
            TransferAccountReleasedFkNavigations = new HashSet<Transfer>();
        }

        public int AccountId { get; set; }
        public int UserFk { get; set; }
        public decimal Balance { get; set; }
        public int RoutingNum { get; set; }
        public DateTime DateCreated { get; set; }
        public string? LName { get; set; }

        public virtual User UserFkNavigation { get; set; } = null!;
        public virtual ICollection<Transfer> TransferAccountReceivedFkNavigations { get; set; }
        public virtual ICollection<Transfer> TransferAccountReleasedFkNavigations { get; set; }
    }
}
