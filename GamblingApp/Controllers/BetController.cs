using Core.Abstractions.Controllers;
using Core.Abstractions.Services;
using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BetController : BaseController<Bet, BetDTO>, IBetController
    {
        private readonly IBetService _betService;
        private readonly IPlayerService _userService;

        public BetController(IBetService betService, IPlayerService userService, IConfiguration configuration, Serilog.ILogger logger) : base(betService, configuration, logger)
        {
            _betService = betService;
            _userService = userService;
        }

        [HttpPost("PlaceBet")]
        public async Task<IActionResult> PlaceBetAsync([FromBody] Bet bet)
        {
            try
            {
                var player = _userService.Get(int.Parse(User.Claims.First(i => i.Type == "UserId").Value)).Result;
                
                try
                {
                    return Ok(await _betService.PlaceBet(bet, player));
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