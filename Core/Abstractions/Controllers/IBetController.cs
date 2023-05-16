using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions.Controllers
{
    public interface IBetController : IBaseController<BetDTO>
    {
        public Task<IActionResult> PlaceBetAsync([FromBody] Bet bet);
    }
}
