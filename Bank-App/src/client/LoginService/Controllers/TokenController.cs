using System.Text;
using System.Text.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.IdentityModel.Tokens;

using LoginService.DTO;
using LoginService.Business;

namespace LoginService.Controllers;

[Route("api/token")]
[ApiController]
[EnableCors(PolicyName ="EnableCors")]
public class TokenConroller : ControllerBase
{
    private ILogic _logic;

    public TokenConroller(ILogic logic)
    {
        _logic = logic;
    }

    [HttpPost("login")]
    public IActionResult Post(LoginDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        JwtSecurityToken? token = null;

        try
        {
            token = _logic.GetJwtSecurityToken(dto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
    }


}
