using login_api.data.Entities;
using Microsoft.Extensions.Options;

namespace login_api.data;

public class DbManager : IDbManager 
{
    public AuthDbContext _context;
    private string ConnectionString {get;set;}

    public DbManager(AuthDbContext context)
    {
        _context = context;
    }

    public DbManager(IOptions<Configuration> options)
    {
        ConnectionString = options.Value.ConnectionString;
    }

    public User GetUser(User user)
    {
        using(_context = new AuthDbContext(ConnectionString)) 
        {
            var response = _context.Users.Where(u => u.Username.Equals(user.Username) && u.Password.Equals(user.Password));

            if(!response.Any())
            {
                return new User();
            }

            return _context.Users
            .Where(u => u.Userid == response.ToList().First().Userid).ToList().First();
        }
    }
}