using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using login_api.DTO;
using login_api.logic;
using Microsoft.AspNetCore.Mvc;

namespace login_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private ILogic _logic;
        public LoginController(ILogic logic)
        {
            _logic = logic;
        }

        [HttpPost("user")]
        public IActionResult GetUser([FromBody] LoginDTO dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                _logic.ConfirmUserCred(dto);
                return Ok("Login Successful");
            }
            catch (Exception ex)
            {
                return BadRequest($"Login Failed." + " " + ex.Message);
            }
            
        }
    }
}