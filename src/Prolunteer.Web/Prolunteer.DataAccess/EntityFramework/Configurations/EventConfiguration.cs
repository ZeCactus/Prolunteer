using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prolunteer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.DataAccess.EntityFramework.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Event> entity)
        {
            entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

            entity.Property(e => e.LocationId).HasColumnName("LocationID");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.EndDate).HasColumnType("datetime");

            entity.Property(e => e.EventTypeId).HasColumnName("EventTypeID");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(e => e.OrganizerId).HasColumnName("OrganizerID");

            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.Property(e => e.Image)
                .HasColumnName("Image")
                .IsRequired(false);

            entity.HasOne(d => d.Location)
                .WithMany(p => p.Events)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Event_Location");

            entity.HasOne(d => d.EventType)
                .WithMany(p => p.Events)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Event_EventType");

            entity.HasOne(d => d.Organizer)
                .WithMany(p => p.Events)
                .HasForeignKey(d => d.OrganizerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Event_User");
        }
    }
}