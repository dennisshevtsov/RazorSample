using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RazorSample.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RazorSample.Data.Configurations
{
  public sealed class ContactEntityTypeConfiguration : IEntityTypeConfiguration<ContactEntity>
  {
    public void Configure(EntityTypeBuilder<ContactEntity> builder)
    {
      builder.HasBaseType<SubjectEntityBase>();

      //builder.Property(entity => entity.ContactId).IsRequired().ValueGeneratedNever().HasColumnName("SubjectId");

      builder.Property(entity => entity.Name).IsRequired().HasMaxLength(256);
      builder.Property(entity => entity.Name).HasMaxLength(256);
    }
  }
}
