using Microsoft.EntityFrameworkCore;
using RazorSample.Data.Entities;

namespace RazorSample.Data.Configurations
{
  public sealed class ClientEntityTypeConfiguration : IEntityTypeConfiguration<ClientEntity>
  {
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ClientEntity> builder)
    {
      builder.ToTable("Clients");
      builder.HasKey(entity => entity.ClientId);

      builder.Property(entity => entity.ClientId).IsRequired().ValueGeneratedNever();
      builder.Property(entity => entity.ClientNo).IsRequired().HasMaxLength(256);
      builder.Property(entity => entity.OrganizationNo).HasMaxLength(256);
      builder.Property(entity => entity.Name).IsRequired().HasMaxLength(256);

      builder.Property(entity => entity.Email).HasMaxLength(256);
      builder.Property(entity => entity.Phone).HasMaxLength(256);
      builder.Property(entity => entity.Address1).HasMaxLength(256);
      builder.Property(entity => entity.Address2).HasMaxLength(256);
      builder.Property(entity => entity.Zip).HasMaxLength(256);
      builder.Property(entity => entity.City).HasMaxLength(256);

      builder.Property(entity => entity.Created).IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql("GETUTCDATE()");
      builder.Property(entity => entity.IsActive).IsRequired().HasDefaultValue(true);
      builder.Property(entity => entity.ClientOwnerId).IsRequired();

      builder.HasOne(entity => entity.ClientOwner).WithMany().HasForeignKey(entity => entity.ClientOwnerId);
    }
  }
}
