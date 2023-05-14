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

        public List<Transfer> ViewTransfers(Int32 releasedFk)
        {
            using(_context = new PostgresContext(ConnectionString))
            {
                var loadQuery = _context.Transfers
                .FromSqlInterpolated
                ($"SELECT * FROM bankapp.transfers WHERE account_released_fk = {releasedFk}").ToList();
                return loadQuery;
            }
        }

        public void TransferAmount(Transfer transfer, int accountReleased, int accountReceived)
        {
            using(_context = new PostgresContext(ConnectionString))
            {
                int generateTransferId = _context.Transfers.Where(t => t.AccountReleasedFk == accountReleased 
                                                                  && t.AccountReceivedFk == accountReceived 
                                                                  && t.DateCreated.Equals(transfer.DateCreated))
                                                                  .ToList().First().TransferId;
                    CreateTransfer(transfer);

            }
        }

        private void CreateTransfer(Transfer transfer)
        {
            _context.Transfers.Add(transfer);
            _context.SaveChanges();
        }
}