using Core.Abstractions.Repository;
using Core.Models;
using Repository;
using Serilog;

namespace Repositories
{
    public class BetRepository : BaseRepository<Bet>, IBetRepository
    {
        public BetRepository(ILogger logger, IGamblingContext context) : base(logger, context)
        {
        }
    }
}