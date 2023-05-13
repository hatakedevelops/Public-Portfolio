using AccountService.Business;

using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class ViewAccountController : ControllerBase
    {
        private ILogic _logic;

        public ViewAccountController(ILogic logic)
        {
            _logic = logic;
        }

        [HttpGet("accounts")]
        public IActionResult ViewAccount(Int32 userFkId)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            } else {
                return Ok(_logic.ViewAccount(userFkId));
            }
        }
    }
