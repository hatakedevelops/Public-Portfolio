using System;
using System.Collections.Generic;

namespace AccountService.Data.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string FName { get; set; } = null!;

    public string LName { get; set; } = null!;

    public string UserAddress { get; set; } = null!;

    public string PhoneNum { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<UserCred> UserCreds { get; set; } = new List<UserCred>();
}
