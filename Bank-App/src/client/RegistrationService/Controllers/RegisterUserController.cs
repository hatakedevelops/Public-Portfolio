using RegistrationService.DTO;
using RegistrationService.Business;

using Microsoft.AspNetCore.Mvc;

namespace RegistrationService.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private ILogic _logic;

        public RegisterUserController(ILogic logic)
        {
            _logic = logic;
        }

        [HttpPost("reg-usr")]
        public IActionResult RegisterUser(RegisterDTO register)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            } else {
                _logic.RegisterUser(register);
                return Ok();
            }
        }
    }