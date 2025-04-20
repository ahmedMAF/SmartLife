using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Json;
using SmartLife.Models;

namespace SmartLife;

public class MySqlDb : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> News { get; set; }
    public DbSet<Contact> Contact { get; set; }
    public DbSet<PartnerClient> PartnersClients { get; set; }
    public DbSet<TeamMember> Team { get; set; }
    public DbSet<Product> Products { get; set; }

    public MySqlDb(DbContextOptions<MySqlDb> options) : base(options)
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
    }
}
