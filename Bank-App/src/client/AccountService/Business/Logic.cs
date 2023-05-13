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
    }