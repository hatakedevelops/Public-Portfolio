using System;
using System.Collections.Generic;

namespace RegistrationService.Data.Entities
{
    public partial class Transfer
    {
        public int TransferId { get; set; }
        public int AccountReleasedFk { get; set; }
        public int AccountReceivedFk { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal AmountTransferred { get; set; }

        public virtual Account AccountReceivedFkNavigation { get; set; } = null!;
        public virtual Account AccountReleasedFkNavigation { get; set; } = null!;
    }
}
