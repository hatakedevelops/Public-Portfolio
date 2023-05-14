using AccountService.DTO;
using AccountService.Data;
using AccountService.Data.Entities;

namespace AccountService.Business;

    public class Logic : ILogic
    {
        private IDbManager _data;

        public Logic(IDbManager data)
        {
            _data = data;
        }

        public List<Account> ViewAccount(Int32 userFkId)
        {
            return _data.ViewAccount(userFkId).ToList();
        }

        public List<Transfer> ViewTransfers(Int32 releasedFk)
        {
            return _data.ViewTransfers(releasedFk).ToList();
        }

        void TransferAmount(TransferDTO receipt) 
        {
            Transfer transfer = new Transfer();
                transfer.AccountReleasedFk = receipt.accountReleasedFk;
                transfer.AccountReceivedFk = receipt.acccountReceivedFk;
                transfer.AmountTransferred = receipt.transferAmount;

            _data.TransferAmount(transfer);
        }
    }