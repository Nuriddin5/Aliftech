using Microsoft.EntityFrameworkCore;
using test25_08.Models;

namespace test25_08;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Wallet>? Wallet { get; set; }
    public DbSet<User>? Users { get; set; }
    
    public DbSet<Recharge>? Recharges { get; set; }
}