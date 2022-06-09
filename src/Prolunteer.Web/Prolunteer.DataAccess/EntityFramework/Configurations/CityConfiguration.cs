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
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<City> entity)
        {
            entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

            entity.Property(e => e.CountyId).HasColumnName("CountyID");

            entity.HasOne(d => d.County)
                .WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_City_County");
        }
    }
}
