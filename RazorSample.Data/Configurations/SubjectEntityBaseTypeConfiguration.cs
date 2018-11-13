using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RazorSample.Data.Entities;

namespace RazorSample.Data.Configurations
{
  public sealed class SubjectEntityBaseTypeConfiguration : IEntityTypeConfiguration<SubjectEntityBase>
  {
    public void Configure(EntityTypeBuilder<SubjectEntityBase> builder)
    {
      builder.ToTable("Subjects");
      builder.HasDiscriminator<string>("SubjectType");

      builder.Property(entity => entity.Created).IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql("GETUTCDATE()");
      builder.Property(entity => entity.IsActive).IsRequired().HasDefaultValue(true);

      builder.HasMany(entity => entity.Emails).WithOne().HasForeignKey().HasForeignKey(entity => entity.SubjectId);
      builder.HasMany(entity => entity.Phones).WithOne().HasForeignKey().HasForeignKey(entity => entity.SubjectId);
      builder.HasMany(entity => entity.Ims).WithOne().HasForeignKey().HasForeignKey(entity => entity.SubjectId);
      builder.HasMany(entity => entity.Addresses).WithOne().HasForeignKey().HasForeignKey(entity => entity.SubjectId);
    }
  }
}
