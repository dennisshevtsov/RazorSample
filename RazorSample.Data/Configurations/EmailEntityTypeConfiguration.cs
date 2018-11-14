using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RazorSample.Data.Entities;
using System;

namespace RazorSample.Data.Configurations
{
  public sealed class EmailEntityTypeConfiguration : IEntityTypeConfiguration<EmailEntity>
  {
    public void Configure(EntityTypeBuilder<EmailEntity> builder)
    {
      builder.ToTable("Emails");

      builder.Property<Guid>("EmailId").IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
      builder.HasKey("EmailId");

      builder.Property(entity => entity.Email).IsRequired().HasMaxLength(256);
      builder.Property(entity => entity.Description).HasMaxLength(256);
      builder.Property(entity => entity.SubjectId).IsRequired();
    }
  }
}
