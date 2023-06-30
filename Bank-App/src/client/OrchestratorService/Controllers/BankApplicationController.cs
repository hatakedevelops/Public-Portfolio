using OrchestratorService.Business;
using OrchestratorService.DTO;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace OrchestratorService.Controllers;

[ApiController]
[Route("/api/[controller]")]
[EnableCors(PolicyName = "EnableCors")]
public class BankApplicationController : ControllerBase
{
    private ILogic _logic;

    public BankApplicationController(ILogic logic)
    {
        _logic = logic;
    }

    [HttpPost("profile")]
    public IActionResult RegisterUser(RegisterDTO register)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            _logic.RequestRegistrationServiceApiAsync(register);
            return Ok();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("transfer")]
    [Authorize]
    public async Task<IActionResult> RequestTransfer(TransferDTO transfer)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            var token = Request.Headers["Authorization"].ToString();
            _logic.RequestAccountServiceApiAsync(transfer, token);

            return Ok();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("account/{userid}")]
    [Authorize]
    public async Task<IActionResult> RequestAccount(int userId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var response = await _logic.RequestAccountServiceApiAsync(userId, token);

            return Ok(response);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("username/checkavailability/{username}")]
    public async Task<IActionResult> CheckUserName(string userName)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var response = await _logic.CheckIfUsernameAvailAsync(userName);

            return Ok();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}