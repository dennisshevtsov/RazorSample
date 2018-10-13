using Microsoft.EntityFrameworkCore;
using RazorSample.Data.Configurations;

namespace RazorSample.Data
{
  public sealed class ConfigurableDbContext : DbContext
  {
    public ConfigurableDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
    }
  }
}
