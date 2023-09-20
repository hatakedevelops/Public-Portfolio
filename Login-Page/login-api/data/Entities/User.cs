using System;
using System.Collections.Generic;

namespace login_api.data.Entities;

public partial class User
{
    public int Userid { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
