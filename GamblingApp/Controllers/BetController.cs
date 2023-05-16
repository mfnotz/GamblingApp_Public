using Core.Abstractions.Controllers;
using Core.Abstractions.Services;
using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BetController : BaseController<Bet, BetDTO>, IBetController
    {
        private readonly IBetService _betService;
        private readonly IUserService _userService;

        public BetController(IBetService betService, IUserService userService, IConfiguration configuration, Serilog.ILogger logger) : base(betService, configuration, logger)
        {
            _betService = betService;
            _userService = userService;
        }

        [HttpPost("bet")]
        public async Task<IActionResult> PlaceBetAsync([FromBody] Bet bet)
        {
            try
            {
                var userName = GetWindowsUsername();
                var user = _userService.GetBy(items => items.Where(predicate: entity => entity.UserName == userName), null, null).Result.FirstOrDefault();

                try
                {
                    return Ok(await _betService.PlaceBet(bet, user));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            catch(Exception ex)
            {
                return BadRequest("The user doesn't exist.");
            }
        }
    }
}