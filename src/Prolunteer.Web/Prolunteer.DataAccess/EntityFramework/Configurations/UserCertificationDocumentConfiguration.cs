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
    public class UserCertificationDocumentConfiguration : IEntityTypeConfiguration<UserCertificationDocument>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserCertificationDocument> entity)
        {
            entity.ToTable("UserCertificationDocuments");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");

            entity.Property(e => e.UserId)
                .HasColumnName("UserID")
                .IsRequired();

            entity.Property(e => e.CertificationId)
                .HasColumnName("CertificationID")
                .IsRequired();

            entity.Property(e => e.Document)
                .IsRequired();

            entity.HasOne(p => p.UserCertification)
                .WithMany(d => d.UserCertificationDocuments)
                .HasForeignKey(d => new
                {
                    d.UserId,
                    d.CertificationId
                })
                .HasConstraintName("FK_UserCertificationDocument_UserCertification");
        }
    }
}
