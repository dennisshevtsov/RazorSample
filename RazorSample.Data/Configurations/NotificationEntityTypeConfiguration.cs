using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RazorSample.Data.Entities;

namespace RazorSample.Data.Configurations
{
  public sealed class NotificationEntityTypeConfiguration : IEntityTypeConfiguration<NotificationEntity>
  {
    public void Configure(EntityTypeBuilder<NotificationEntity> builder)
    {
      builder.ToTable("Notifications");

      builder.Property(entity => entity.NotificationId).IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
      builder.HasKey(entity => entity.NotificationId);

      builder.Property(entity => entity.Title).IsRequired().HasMaxLength(256);
      builder.Property(entity => entity.Created).IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql("GETUTCDATE()");
      builder.Property(entity => entity.Closed);

      builder.Property(entity => entity.SubjectId).IsRequired();
    }
  }
}
