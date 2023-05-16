using Core.Abstractions.Repository;
using Core.Models;
using Microsoft.EntityFrameworkCore;

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
            modelBuilder.Entity<Player>()
            .HasKey(p => p.Id);

            modelBuilder.Entity<UserInfo>()
                .HasKey(ui => ui.Id);
            modelBuilder.Entity<Bet>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<UserInfo>()
                .HasOne(ui => ui.Player)
                .WithOne(p => p.User)
                .HasForeignKey<Player>(p => p.UserId);
        }

        public DbSet<Bet> Bet { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<UserInfo> UserInfo { get; set;}

    }
}
