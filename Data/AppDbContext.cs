using Microsoft.EntityFrameworkCore;
using WasteGlassAPI.Models;

namespace WasteGlassAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Supplier>   Suppliers   { get; set; }
    public DbSet<Collection> Collections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Supplier>()
            .HasMany(s => s.Collections)
            .WithOne(c => c.Supplier)
            .HasForeignKey(c => c.SupplierId);
    }
}
