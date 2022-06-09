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
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> entity)
        {
            entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

            entity.Property(e => e.EMail)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(65)
                .IsUnicode(false)
                .IsFixedLength(true);
        }
    }
}
