using System.IdentityModel.Tokens.Jwt;
using LoginService.Data.Entities;
using LoginService.DTO;

namespace LoginService.Business;

public interface ILogic
{
    User GetUser(LoginDTO dto);
    JwtSecurityToken GetJwtSecurityToken(LoginDTO dto);
}