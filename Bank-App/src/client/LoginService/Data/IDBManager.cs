using LoginService.Data.Entities;

namespace LoginService.Data;

public interface IDbManager
{
    User GetUser(UserCred cred);
    string GetSaltUsed(string userName);
}