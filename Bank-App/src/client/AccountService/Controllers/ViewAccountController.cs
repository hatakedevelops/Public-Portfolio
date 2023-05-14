using AccountService.DTO;
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

        [HttpGet("trans-recpts")]
        public IActionResult ViewTransfers(Int32 releasedFk)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            } else {
                return Ok(_logic.ViewTransfers(releasedFk));
            }
        }

        [HttpPost("transfer")]
        public IActionResult TransferAmount(TransferDTO receipt)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            } else {
                _logic.TransferAmount(receipt);
                return Ok("Transfer Complete");
            }
        }
    }
