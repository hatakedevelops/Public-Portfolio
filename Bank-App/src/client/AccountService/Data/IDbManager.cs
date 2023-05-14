using AccountService.Data.Entities;

namespace AccountService.Data;

    public interface IDbManager
    {
            List<Account> ViewAccount(Int32 userFkId);

            List<Transfer> ViewTransfers(Int32 releasedFk);

            void TransferAmount(Transfer transfer) {}
    }