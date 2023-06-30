using OrchestratorService.DTO;

namespace OrchestratorService.Business;

public interface ILogic
{
    Task<List<TransferDTO>> RequestAccountServiceApiAsync(int userId, string token);
    void RequestAccountServiceApiAsync(TransferDTO transfer, string token);
    void RequestRegistrationServiceApiAsync(RegisterDTO register);
    Task<bool> CheckIfUsernameAvailAsync(string userName);
}