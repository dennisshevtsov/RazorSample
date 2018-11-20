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

      builder.Property(entity => entity.AddressId).IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
      builder.Property(entity => entity.Address).IsRequired().HasMaxLength(256);
      builder.Property(entity => entity.Zip).HasMaxLength(256);
      builder.Property(entity => entity.City).HasMaxLength(256);
      builder.Property(entity => entity.Country).HasMaxLength(256);

      builder.Property(entity => entity.Description).HasMaxLength(256);
      builder.Property(entity => entity.SubjectId).IsRequired();

      builder.HasKey(entity => entity.AddressId);
    }
  }
}
