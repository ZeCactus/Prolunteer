using Microsoft.EntityFrameworkCore;
using Prolunteer.Entities;

namespace Prolunteer.DataAccess.EntityFramework.Configurations
{
    class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Location> entity)
        {
            entity.ToTable("Locations");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");

            entity.Property(e => e.CityId).HasColumnName("CityID");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(40);

            entity.Property(e => e.Description)
                .HasMaxLength(200);

            entity.HasOne(d => d.City)
                .WithMany(p => p.Locations)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Location_City");
        }
    }
}
