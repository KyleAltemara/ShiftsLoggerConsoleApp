using Microsoft.EntityFrameworkCore;

namespace ShiftsLoggerAPI.Models;

public class ShiftLoggerDbContext : DbContext
{
    public DbSet<ShiftLog> ShiftLogs { get; set; } = null!;

    public ShiftLoggerDbContext(DbContextOptions<ShiftLoggerDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    /// <summary>
    /// Configures the relationships between the entities.
    /// </summary>
    /// <param name="modelBuilder"> The <see cref="ModelBuilder"/> to use for configuration. </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.Entity<ShiftLog>();
}
