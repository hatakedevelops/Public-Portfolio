using System;

namespace AccountService.Exceptions
{
    public class CheckAccountSameException : Exception
    {
        public CheckAccountSameException(string errorMessage): base(errorMessage) {}
    }
}