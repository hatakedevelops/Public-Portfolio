using AccountService.Data.Entities;

namespace AccountService.Business;

    public interface ILogic
    {
            List<Account> ViewAccount(Int32 userFkId);
    }