using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TourDeApp.Models;

public class UserDatabaseContext : IdentityDbContext
{
    public UserDatabaseContext(DbContextOptions<UserDatabaseContext> options) : base(options) {}
    
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=users.db");
        }
    }
}