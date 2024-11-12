using Microsoft.EntityFrameworkCore;
using TourDeApp.Models.DataBaseModels;

namespace TourDeApp;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<GameDb> Games { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<CellDb>()
            .Property(x => x.State)
            .HasConversion<string>();
        
        modelBuilder.Entity<GameDb>()
            .Property(x => x.GameState)
            .HasConversion<string>();
        
        modelBuilder.Entity<GameDb>()
            .HasOne(g => g.GameBoard)
            .WithOne(gb => gb.Game)
            .HasForeignKey<GameDb>(g => g.GameBoardId);

        modelBuilder.Entity<GameBoardDb>()
            .HasOne(gb => gb.Game)
            .WithOne(g => g.GameBoard)
            .HasForeignKey<GameBoardDb>(gb => gb.GameId);
    }
}