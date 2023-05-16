using AccountService.DTO;
using AccountService.Business;
using AccountService.Exceptions;

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

        [HttpGet("accounts/{userFkId}")]
        public IActionResult ViewAccounts(Int32 userFkId)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            } else {
                return Ok(_logic.ViewAccounts(userFkId));
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
            } 

            try {
                _logic.TransferAmount(receipt);
            } catch(CheckIfAccountsExistException ex){
                return BadRequest(ex.Message);
            } catch(CheckAccountSameException acc) {
                return BadRequest(acc.Message);
            }
            return Ok("Transfer Successful");
        }
    }
