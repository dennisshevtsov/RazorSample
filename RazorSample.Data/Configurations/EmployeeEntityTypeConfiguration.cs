using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RazorSample.Data.Entities;

namespace RazorSample.Data.Configurations
{
  public sealed class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
  {
    public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
    {
      builder.ToTable("Employees");
      builder.HasKey(entity => entity.EmployeeId);
      builder.HasBaseType<SubjectEntityBase>();

      builder.Property(entity => entity.EmployeeId).IsRequired().ValueGeneratedNever();
      builder.Property(entity => entity.FirstName).IsRequired().HasMaxLength(256);
      builder.Property(entity => entity.LastName).IsRequired().HasMaxLength(256);
      builder.Property(entity => entity.EmployeeNo).IsRequired().HasMaxLength(256);

      builder.Ignore(entity => entity.FullName);
    }
  }
}
