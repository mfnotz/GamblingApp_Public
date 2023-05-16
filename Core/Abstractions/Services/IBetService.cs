using Core.DTOs;
using Core.Models;

namespace Core.Abstractions.Services
{
    public interface IBetService : IBaseService<Bet, BetDTO> //
    {
        public Task<BetResult> PlaceBet(Bet bet, PlayerDTO userDTO);
    }
}
