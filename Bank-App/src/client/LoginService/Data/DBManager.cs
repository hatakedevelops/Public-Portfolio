using LoginService.Data.Entities;
using Microsoft.Extensions.Options;

namespace LoginService.Data;

public class DbManager : IDbManager
{
    private PostgresContext _context;
    private string ConnectionString {get; set;}

    public DbManager(IOptions<Configuration.Configuration> options)
    {
        ConnectionString = options.Value.ConnectionString;
    }

    public DbManager(PostgresContext context)
    {
        _context = context;
    }

    public User GetUser(UserCred cred)
    {
        using (_context = new PostgresContext(ConnectionString))
        {
            var response = _context
            .UserCreds.Where(u => u.UserName.Equals(cred.UserName) && u.PassHash.Equals(cred.PassHash));

            if(!response.Any())
            {
                return new User();
            }

            return _context.Users
            .Where(u => u.UserId == response.ToList().First().UserCredId).ToList().First();

        }
    }
    public string GetSaltUsed(string userName)
    {
        using (_context = new PostgresContext(ConnectionString))
        {
            var response = _context.UserCreds.Where(u => u.UserName.Equals(userName));

            if(!response.Any())
            {
                return string.Empty;
            }

            return response.ToList().First().PassHash;
        }
    }
}