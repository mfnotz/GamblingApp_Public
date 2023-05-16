using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Net;

namespace Core.Abstractions.Repository
{
    public interface IGamblingContext : IDbContext
    {
        DbSet<Bet> Bet { get; set; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellation = default);
    }
}
