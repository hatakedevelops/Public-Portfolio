using System;
using System.Collections.Generic;

namespace RegistrationService.Data.Entities
{
    public partial class User
    {
        public User()
        {
            Accounts = new HashSet<Account>();
            UserCreds = new HashSet<UserCred>();
        }

        public int UserId { get; set; }
        public string FName { get; set; } = null!;
        public string LName { get; set; } = null!;
        public string UserAddress { get; set; } = null!;
        public string PhoneNum { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<UserCred> UserCreds { get; set; }
    }
}
