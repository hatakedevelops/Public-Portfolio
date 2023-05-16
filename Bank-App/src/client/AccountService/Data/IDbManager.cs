using AccountService.Data.Entities;

namespace AccountService.Data;

    public interface IDbManager
    {
            List<Account> ViewAccounts(Int32 userFkId);

            Account GetAccount(int accountId);

            List<Transfer> ViewTransfers(Int32 releasedFk);

            void TransferAmount(Transfer transfer) {}
    }