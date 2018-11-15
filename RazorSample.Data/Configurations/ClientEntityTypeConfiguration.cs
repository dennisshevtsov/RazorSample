using Microsoft.EntityFrameworkCore;
using RazorSample.Data.Entities;

namespace RazorSample.Data.Configurations
{
  public sealed class ClientEntityTypeConfiguration : IEntityTypeConfiguration<ClientEntity>
  {
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ClientEntity> builder)
    {
      builder.HasBaseType<SubjectEntityBase>();

      //builder.Property<Guid>("ClientId").IsRequired().ValueGeneratedNever().HasColumnName("SubjectId").HasColumnType("uniqueidentifier");

      builder.Property(entity => entity.ClientNo).IsRequired().HasMaxLength(256);
      builder.Property(entity => entity.OrganizationNo).HasMaxLength(256);
      builder.Property(entity => entity.Name).IsRequired().HasMaxLength(256);

      builder.Property(entity => entity.ClientOwnerId).IsRequired();
      builder.HasOne(entity => entity.ClientOwner).WithMany().HasForeignKey(entity => entity.ClientOwnerId).OnDelete(DeleteBehavior.Restrict);

      builder.Ignore(entity => entity.ClientId);
    }
  }
}
