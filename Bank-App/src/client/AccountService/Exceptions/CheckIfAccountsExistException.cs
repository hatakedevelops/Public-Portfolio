using System;

namespace AccountService.Exceptions
{
    public class CheckIfAccountsExistException : Exception
    {
        public CheckIfAccountsExistException(string errorMessage) : base(errorMessage) {}
    }
}