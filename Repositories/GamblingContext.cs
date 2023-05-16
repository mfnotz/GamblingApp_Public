using Core.Abstractions.Repository;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Repository
{
    public class GamblingContext : DbContext, IGamblingContext
    {
        public GamblingContext(DbContextOptions<GamblingContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Bet> Bet { get; set; }
        public DbSet<User> User { get; set; }

    }
}
