using Microsoft.EntityFrameworkCore;
using RazorSample.Data.Configurations;

namespace RazorSample.Data
{
  public sealed class ConfigurableDbContext : DbContext
  {
    public ConfigurableDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new SubjectEntityBaseTypeConfiguration());
      modelBuilder.ApplyConfiguration(new EmailEntityTypeConfiguration());
      modelBuilder.ApplyConfiguration(new PhoneEntityTypeConfiguration());
      modelBuilder.ApplyConfiguration(new ImEntityTypeConfiguration());
      modelBuilder.ApplyConfiguration(new AddressEntityTypeConfiguration());

      modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
      modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration());
    }
  }
}
