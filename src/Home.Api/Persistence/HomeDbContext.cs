using Home.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Home.Api.Persistence;

public sealed class HomeDbContext(DbContextOptions<HomeDbContext> options) : DbContext(options)
{
    public DbSet<HomeProfile> HomeProfiles => Set<HomeProfile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HomeProfile>(entity =>
        {
            entity.HasKey(home => home.Id);
            entity.Property(home => home.DisplayName)
                .HasMaxLength(200)
                .IsRequired();
            entity.HasIndex(home => home.DisplayName);
        });
    }
}