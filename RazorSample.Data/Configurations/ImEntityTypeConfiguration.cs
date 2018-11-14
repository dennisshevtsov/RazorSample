using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RazorSample.Data.Entities;
using System;

namespace RazorSample.Data.Configurations
{
  public sealed class ImEntityTypeConfiguration : IEntityTypeConfiguration<ImEntity>
  {
    public void Configure(EntityTypeBuilder<ImEntity> builder)
    {
      builder.ToTable("Ims");

      builder.Property<Guid>("ImId").IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
      builder.Property(entity => entity.Im).IsRequired().HasMaxLength(256);

      builder.Property(entity => entity.Description).HasMaxLength(256);
      builder.Property(entity => entity.SubjectId).IsRequired();

      builder.HasKey("ImId");
    }
  }
}
