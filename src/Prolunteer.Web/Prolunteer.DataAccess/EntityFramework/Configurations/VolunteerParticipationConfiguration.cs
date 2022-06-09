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
    class VolunteerParticipationConfiguration : IEntityTypeConfiguration<VolunteerParticipation>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VolunteerParticipation> entity)
        {
            entity.HasKey(e => new { e.VolunteerPositionId, e.UserId });

            entity.Property(e => e.VolunteerPositionId).HasColumnName("VolunteerPositionID");

            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User)
                .WithMany(p => p.VolunteerParticipations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VolunteerParticipation_User");

            entity.HasOne(d => d.VolunteerPosition)
                .WithMany(p => p.VolunteerParticipations)
                .HasForeignKey(d => d.VolunteerPositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VolunteerParticipation_VolunteerPosition");
        }
    }
}
