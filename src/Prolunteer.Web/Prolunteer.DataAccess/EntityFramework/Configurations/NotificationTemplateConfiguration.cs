using Microsoft.EntityFrameworkCore;
using Prolunteer.Entities;
using System;

namespace Prolunteer.DataAccess.EntityFramework.Configurations
{
    public class NotificationTemplateConfiguration : IEntityTypeConfiguration<NotificationTemplate>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<NotificationTemplate> entity)
        {
            entity.ToTable("NotificationTemplates");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            entity.Property(e => e.NotificationName)
                .IsRequired()
                .HasMaxLength(40);

            entity.Property(e => e.Subject)
                .IsRequired()
                .HasMaxLength(40);

            entity.Property(e => e.Template)
                .IsRequired();
        }
    }
}
