using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RazorSample.Data.Entities;

namespace RazorSample.Data.Configurations
{
  public sealed class AddressEntityTypeConfiguration : IEntityTypeConfiguration<AddressEntity>
  {
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
      builder.ToTable("Addresses");
      builder.HasKey("AddressId");

      builder.Property(entity => entity.Address).IsRequired().HasMaxLength(256);
      builder.Property(entity => entity.Zip).IsRequired().HasMaxLength(256);
      builder.Property(entity => entity.City).IsRequired().HasMaxLength(256);
      builder.Property(entity => entity.Country).IsRequired().HasMaxLength(256);

      builder.Property(entity => entity.Description).HasMaxLength(256);
      builder.Property(entity => entity.SubjectId).IsRequired();
    }
  }
}
