using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RazorSample.Data.Entities;

namespace RazorSample.Data.Configurations
{
  public sealed class ImEntityTypeConfiguration : IEntityTypeConfiguration<ImEntity>
  {
    public void Configure(EntityTypeBuilder<ImEntity> builder)
    {
      builder.ToTable("Ims");

      builder.Property(entity => entity.ImId).IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
      builder.HasKey(entity => entity.ImId);

      builder.Property(entity => entity.Im).IsRequired().HasMaxLength(256);
      builder.Property(entity => entity.Description).HasMaxLength(256);
      builder.Property(entity => entity.SubjectId).IsRequired();
    }
  }
}
