using DominoTrains.Domain.Aggregates;
using DominoTrains.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DominoTrains.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Game> Games { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>(e =>
        {
            e.OwnsOne(g => g.Player, player => player.OwnsMany(p => p.Dominoes));
            e.HasOne(g => g.TrainStation);
        });

        modelBuilder.Entity<TrainStation>(e =>
        {
            e.HasOne(s => s.North);
            e.HasOne(s => s.East);
            e.HasOne(s => s.West);
            e.HasOne(s => s.South);
        });

        modelBuilder.Entity<Train>(e =>
        {
            e.OwnsMany(t => t.Dominoes);
        });
    }
}