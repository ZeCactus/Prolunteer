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
    class RequiredCertificationConfiguration : IEntityTypeConfiguration<RequiredCertification>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RequiredCertification> entity)
        {
            entity.HasKey(e => new { e.VolunteerPositionId, e.CertificationId });

            entity.Property(e => e.VolunteerPositionId).HasColumnName("VolunteerPositionID");

            entity.Property(e => e.CertificationId).HasColumnName("CertificationID");

            entity.HasOne(d => d.Certification)
                .WithMany(p => p.RequiredCertifications)
                .HasForeignKey(d => d.CertificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequiredCertification_Certification");

            entity.HasOne(d => d.VolunteerPosition)
                .WithMany(p => p.RequiredCertifications)
                .HasForeignKey(d => d.VolunteerPositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequiredCertification_VolunteerPosition");
        }
    }
}
