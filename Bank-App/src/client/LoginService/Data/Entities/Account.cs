using System;
using System.Collections.Generic;

namespace LoginService.Data.Entities;

public partial class Account
{
    public int AccountId { get; set; }

    public int UserFk { get; set; }

    public decimal Balance { get; set; }

    public int RoutingNum { get; set; }

    public DateTime DateCreated { get; set; }

    public string? LName { get; set; }

    public virtual ICollection<Transfer> TransferAccountReceivedFkNavigations { get; set; } = new List<Transfer>();

    public virtual ICollection<Transfer> TransferAccountReleasedFkNavigations { get; set; } = new List<Transfer>();

    public virtual User UserFkNavigation { get; set; } = null!;
}
