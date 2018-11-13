using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RazorSample.Data.Entities;

namespace RazorSample.Data.Configurations
{
  public sealed class EmailEntityTypeConfiguration : IEntityTypeConfiguration<EmailEntity>
  {
    public void Configure(EntityTypeBuilder<EmailEntity> builder)
    {
      builder.ToTable("Emails");
      builder.HasKey("EmailId");

      builder.Property(entity => entity.Email).IsRequired().HasMaxLength(256);

      builder.Property(entity => entity.Description).HasMaxLength(256);
      builder.Property(entity => entity.SubjectId).IsRequired();
    }
  }
}
