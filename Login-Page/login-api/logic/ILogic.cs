using login_api.data.Entities;
using login_api.DTO;

namespace login_api.logic;

public interface ILogic
{
    User ConfirmUserCred(LoginDTO dto);
}