using Microsoft.EntityFrameworkCore;

namespace Core.Abstractions.Repository
{
    public interface IDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
