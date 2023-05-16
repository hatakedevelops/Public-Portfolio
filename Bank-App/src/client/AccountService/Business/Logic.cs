using AccountService.DTO;
using AccountService.Data;
using AccountService.Exceptions;
using AccountService.Data.Entities;

namespace AccountService.Business;

    public class Logic : ILogic
    {
        private IDbManager _data;

        public Logic(IDbManager data)
        {
            _data = data;
        }

        public List<Account> ViewAccounts(Int32 userFkId)
        {
            return _data.ViewAccounts(userFkId).ToList();
        }

        public List<Transfer> ViewTransfers(Int32 releasedFk)
        {
            return _data.ViewTransfers(releasedFk).ToList();
        }

        public void TransferAmount(TransferDTO receipt)
        {
            var sender = _data.GetAccount(receipt.accountReleasedFk);
            var receiver = _data.GetAccount(receipt.acccountReceivedFk);

            Transfer transfer = new Transfer();

                transfer.AccountReleasedFk = receipt.accountReleasedFk;
                transfer.AccountReceivedFk = receipt.acccountReceivedFk;
                transfer.AmountTransferred = receipt.transferAmount;
            
            CheckAccountsExist(sender, receiver);

            ThreadStart postNewTransfer = () =>
            {
                Thread.Sleep(TimeSpan.FromMinutes(2));
                _data.TransferAmount(transfer);
            };

            var thread = new Thread(postNewTransfer);
            thread.Start();
        }

         private void CheckAccountsExist(Account sender, Account receiver)
         {
            if(sender == null ||
               sender.AccountId <=0 ||
               receiver == null ||
               receiver.AccountId <= 0)
               {
                    throw new CheckIfAccountsExistException("Sender or Receiver doesn't Exist.");
               }
         }

         private void CheckAccountsSame(Transfer transfer)
         {
            if(transfer.AccountReceivedFk == transfer.AccountReleasedFk)
            {
                throw new CheckAccountSameException("Can't transfer to the same account.");
            }
         }
    }