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
    class UserCertificationConfiguration : IEntityTypeConfiguration<UserCertification>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserCertification> entity)
        {
            entity.HasKey(e => new { e.UserId, e.CertificationId });

            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.Property(e => e.CertificationId).HasColumnName("CertificationID");

            entity.HasOne(d => d.Certification)
                .WithMany(p => p.UserCertifications)
                .HasForeignKey(d => d.CertificationId)
                .HasConstraintName("FK_UserCertification_Certification");

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserCertifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserCertification_User");
        }
    }
}
