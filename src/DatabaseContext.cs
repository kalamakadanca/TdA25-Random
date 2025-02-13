using Microsoft.EntityFrameworkCore;
using TourDeApp.Models.DataBaseModels;

namespace TourDeApp;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<GameDb> Games { get; set; }
    public DbSet<GameDb> PredefinedGames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GameDb>()
            .Property(x => x.GameState)
            .HasConversion<string>();
    }
}
