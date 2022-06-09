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
    class VolunteerPositionConfiguration : IEntityTypeConfiguration<VolunteerPosition>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VolunteerPosition> entity)
        {
            entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.EventId).HasColumnName("EventID");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(20);

            entity.HasOne(d => d.Event)
                .WithMany(p => p.VolunteerPositions)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VolunteerPosition_Event");
        }
    }
}
