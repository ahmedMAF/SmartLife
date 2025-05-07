using Microsoft.EntityFrameworkCore;
using SmartLife.Models;

namespace SmartLife;

public class SmartLifeDb : DbContext
{
    public DbSet<Post> News { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<PartnerClient> PartnersClients { get; set; }
    public DbSet<TeamMember> Team { get; set; }
    public DbSet<Product> Products { get; set; }

    public SmartLifeDb(DbContextOptions<SmartLifeDb> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .Property(p => p.Features)
            .HasColumnType("json");

        modelBuilder.Entity<Product>()
            .Property(p => p.Models)
            .HasColumnType("json");

        modelBuilder.Entity<Product>()
            .Property(p => p.Photos)
            .HasColumnType("json");

        modelBuilder.Entity<Product>()
            .Property(p => p.Videos)
            .HasColumnType("json");
    }
}
