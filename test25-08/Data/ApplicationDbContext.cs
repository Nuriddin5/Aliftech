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
        modelBuilder.Entity<User>()
            .HasOne(b => b.Wallet2)
            .WithOne(i => i.Owner);
        // .HasForeignKey<User>(b => b.);
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }

    public DbSet<test25_08.Models.Wallet2>? Wallet2 { get; set; }
    public DbSet<Wallet>? Wallet { get; set; }
    public DbSet<User?>? Users { get; set; }
}