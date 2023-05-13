using AccountService.Data.Entities;

using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Data;

    public class DbManager : IDbManager
    {
        private string ConnectionString {get;set;}

        private PostgresContext _context;

        public DbManager(PostgresContext context)
        {
            _context = context;
        }

        public DbManager(IOptions<Configuration> options)
        {
            ConnectionString = options.Value.ConnectionString;
        }


        public List<Account> ViewAccount(Int32 userFkId)
        {
            using(_context = new PostgresContext(ConnectionString))
            {
                var loadQuery = _context.Accounts
                .FromSqlInterpolated
                ($"SELECT * FROM bankapp.accounts WHERE user_fk = {userFkId} ").ToList();
                return loadQuery;
            }
        }

}