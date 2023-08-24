using RegistrationService.Data.Entities;

namespace RegistrationService.Data;

    public interface IDbManager
    {
        void RegisterUser(User user, UserCred cred) {}
    }