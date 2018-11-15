using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RazorSample.Data.Entities;
using System;

namespace RazorSample.Data.Configurations
{
  public sealed class SubjectEntityBaseTypeConfiguration : IEntityTypeConfiguration<SubjectEntityBase>
  {
    public void Configure(EntityTypeBuilder<SubjectEntityBase> builder)
    {
      builder.ToTable("Subjects");
      builder.HasDiscriminator<string>("SubjectType");

      builder.Property<Guid>("SubjectId").IsRequired().ValueGeneratedNever().HasColumnName("SubjectId").HasColumnType("uniqueidentifier").HasField("_subjectId");
      builder.HasKey("SubjectId");

      builder.Property(entity => entity.Created).IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql("GETUTCDATE()");
      builder.Property(entity => entity.IsActive).IsRequired().HasDefaultValue(true);

      builder.HasMany(entity => entity.Emails).WithOne().HasForeignKey(entity => entity.SubjectId).HasForeignKey(entity => entity.SubjectId);
      builder.HasMany(entity => entity.Phones).WithOne().HasForeignKey(entity => entity.SubjectId).HasForeignKey(entity => entity.SubjectId);
      builder.HasMany(entity => entity.Ims).WithOne().HasForeignKey(entity => entity.SubjectId).HasForeignKey(entity => entity.SubjectId);
      builder.HasMany(entity => entity.Addresses).WithOne().HasForeignKey(entity => entity.SubjectId).HasForeignKey(entity => entity.SubjectId);
    }
  }
}
