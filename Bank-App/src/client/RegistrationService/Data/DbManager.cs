using Microsoft.Extensions.Options;
using RegistrationService.Data.Entities;

namespace RegistrationService.Data;

    public class DbManager : IDbManager
    {
        private string ConnectionString {get; set;}

        private postgresContext _context;

        private DbManager(postgresContext context)
        {
            _context = context;
        }

        public DbManager(IOptions<Configuration> options)
        {
            ConnectionString = options.Value.ConnectionString;
        }

        public void RegisterUser(User user, UserCred cred)
        {
            using (_context = new postgresContext(ConnectionString))
            {
                using (var dbTransact = _context.Database.BeginTransaction())
                {
                    CreateUser(user);
                    var generateUserFk = _context.Users
                    .Where(u => u.Email.Equals(user.Email)).FirstOrDefault().UserId;
                    AddUserCred(cred, generateUserFk);
                }
            }
        }

        private void AddUserCred(UserCred cred, int userFkId)
        {
            cred.UserFk = userFkId;
            CreateUserCred(cred);
        }

        private void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        private void CreateUserCred(UserCred cred)
        {
            _context.UserCreds.Add(cred);
            _context.SaveChanges();
        }
    }