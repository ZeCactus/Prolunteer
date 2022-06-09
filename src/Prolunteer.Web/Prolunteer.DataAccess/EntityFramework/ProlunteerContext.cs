using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Prolunteer.DataAccess.EntityFramework.Configurations;
using Prolunteer.Entities;

#nullable disable

namespace Prolunteer.DataAccess.EntityFramework
{
    public partial class ProlunteerContext : DbContext
    {

        public ProlunteerContext()
        {
        }

        public ProlunteerContext(DbContextOptions<ProlunteerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Certification> Certifications { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<County> Counties { get; set; }
        public virtual DbSet<Location> Location { get; set; } 
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<RequiredCertification> RequiredCertifications { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserCertification> UserCertifications { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<VolunteerParticipation> VolunteerParticipations { get; set; }
        public virtual DbSet<VolunteerPosition> VolunteerPositions { get; set; }
        public virtual DbSet<UserCertificationDocument> UserCertification { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.ApplyConfiguration(new CertificationConfiguration());

            modelBuilder.ApplyConfiguration(new CityConfiguration());

            modelBuilder.ApplyConfiguration(new CountyConfiguration());

            modelBuilder.ApplyConfiguration(new EventConfiguration());

            modelBuilder.ApplyConfiguration(new EventTypeConfiguration());

            modelBuilder.ApplyConfiguration(new RequiredCertificationConfiguration());

            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new UserCertificationConfiguration());

            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

            modelBuilder.ApplyConfiguration(new VolunteerParticipationConfiguration());

            modelBuilder.ApplyConfiguration(new VolunteerPositionConfiguration());

            modelBuilder.ApplyConfiguration(new UserCertificationDocumentConfiguration());

            modelBuilder.ApplyConfiguration(new LocationConfiguration());

            modelBuilder.ApplyConfiguration(new NotificationTemplateConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}