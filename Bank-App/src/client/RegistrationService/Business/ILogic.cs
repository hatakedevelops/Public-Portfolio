using RegistrationService.DTO;

namespace RegistrationService.Business;

    public interface ILogic
    {
        void RegisterUser(RegisterDTO register);
    }