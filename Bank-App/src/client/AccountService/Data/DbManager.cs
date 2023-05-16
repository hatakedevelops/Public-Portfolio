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


        public List<Account> ViewAccounts(Int32 userFkId)
        {
            using(_context = new PostgresContext(ConnectionString))
            {
                var loadQuery = _context.Accounts
                .FromSqlInterpolated
                ($"SELECT * FROM bankapp.accounts WHERE user_fk = {userFkId} ").ToList();
                return loadQuery;
            }
        }

        public Account GetAccount(int accountId)
        {
            using(_context = new PostgresContext(ConnectionString))
            {
                var account = _context.Accounts.Where(a => a.AccountId == accountId).ToList();
                if(!account.Any())
                {
                    return new Account();
                }

                return account.First();
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

        public void TransferAmount(Transfer transfer, int accountReleased, int accountReceived, bool updateTransfer = false)
        {
            using(_context = new PostgresContext(ConnectionString))
            {
                using(var trans = _context.Database.BeginTransaction())
                {

                    if(!updateTransfer)
                    {
                        CreateTransfer(transfer);
                    } else {
                        UpdateTransfer(transfer, accountReleased, accountReceived);
                    }

                    trans.Commit();
                }
            }
        }

        private void CreateTransfer(Transfer transfer)
        {
            _context.Transfers.Add(transfer);
            _context.SaveChanges();
        }

        private void UpdateTransfer(Transfer transfer, int accountReleased, int accountReceived)
        {
            int generateTransferId = _context.Transfers.Where(t => t.AccountReleasedFk == accountReleased 
                                                            && t.AccountReceivedFk == accountReceived 
                                                            && t.DateCreated.Equals(transfer.DateCreated))
                                                            .ToList().First().TransferId;    
            transfer.TransferId = generateTransferId;

            _context.Transfers.Update(transfer);
            _context.SaveChanges();   
        }

}