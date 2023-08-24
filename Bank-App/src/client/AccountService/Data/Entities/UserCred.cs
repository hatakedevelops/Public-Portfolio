using System;
using System.Collections.Generic;

namespace AccountService.Data.Entities;

public partial class UserCred
{
    public int UserCredId { get; set; }

    public int UserFk { get; set; }

    public string? UserName { get; set; }

    public Guid Salt { get; set; }

    public string PassHash { get; set; } = null!;

    public virtual User UserFkNavigation { get; set; } = null!;
}
