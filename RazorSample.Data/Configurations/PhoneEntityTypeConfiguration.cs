﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RazorSample.Data.Entities;

namespace RazorSample.Data.Configurations
{
  public sealed class PhoneEntityTypeConfiguration : IEntityTypeConfiguration<PhoneEntity>
  {
    public void Configure(EntityTypeBuilder<PhoneEntity> builder)
    {
      builder.ToTable("Phones");
      builder.HasKey("PhoneId");

      builder.Property(entity => entity.Phone).IsRequired().HasMaxLength(256);

      builder.Property(entity => entity.Description).HasMaxLength(256);
      builder.Property(entity => entity.SubjectId).IsRequired();
    }
  }
}
