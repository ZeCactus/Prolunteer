﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Prolunteer.DataAccess.EntityFramework;

namespace Prolunteer.DataAccess.Migrations
{
    [DbContext(typeof(ProlunteerContext))]
    [Migration("20210825125326_Added user certification documents table")]
    partial class Addedusercertificationdocumentstable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Prolunteer.Entities.Certification", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Certifications");
                });

            modelBuilder.Entity("Prolunteer.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<Guid>("CountyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CountyID");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountyId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Prolunteer.Entities.County", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Counties");
                });

            modelBuilder.Entity("Prolunteer.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CityID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<int>("EventTypeId")
                        .HasColumnType("int")
                        .HasColumnName("EventTypeID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("OrganizerID");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("EventTypeId");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Prolunteer.Entities.EventType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("EventTypes");
                });

            modelBuilder.Entity("Prolunteer.Entities.RequiredCertification", b =>
                {
                    b.Property<Guid>("VolunteerPositionId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("VolunteerPositionID");

                    b.Property<int>("CertificationId")
                        .HasColumnType("int")
                        .HasColumnName("CertificationID");

                    b.HasKey("VolunteerPositionId", "CertificationId");

                    b.HasIndex("CertificationId");

                    b.ToTable("RequiredCertifications");
                });

            modelBuilder.Entity("Prolunteer.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Prolunteer.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(65)
                        .IsUnicode(false)
                        .HasColumnType("char(65)")
                        .IsFixedLength(true);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Prolunteer.Entities.UserCertification", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserID");

                    b.Property<int>("CertificationId")
                        .HasColumnType("int")
                        .HasColumnName("CertificationID");

                    b.Property<bool>("Approved")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "CertificationId");

                    b.HasIndex("CertificationId");

                    b.ToTable("UserCertifications");
                });

            modelBuilder.Entity("Prolunteer.Entities.UserCertificationDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<int>("CertificationId")
                        .HasColumnType("int")
                        .HasColumnName("CertificationID");

                    b.Property<byte[]>("Document")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserID");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "CertificationId");

                    b.ToTable("UserCertificationDocuments");
                });

            modelBuilder.Entity("Prolunteer.Entities.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserID");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("RoleID");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Prolunteer.Entities.VolunteerParticipation", b =>
                {
                    b.Property<Guid>("VolunteerPositionId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("VolunteerPositionID");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserID");

                    b.HasKey("VolunteerPositionId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("VolunteerParticipations");
                });

            modelBuilder.Entity("Prolunteer.Entities.VolunteerPosition", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EventID");

                    b.Property<int>("MaximumNrOfVolunteers")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("VolunteerPositions");
                });

            modelBuilder.Entity("Prolunteer.Entities.City", b =>
                {
                    b.HasOne("Prolunteer.Entities.County", "County")
                        .WithMany("Cities")
                        .HasForeignKey("CountyId")
                        .HasConstraintName("FK_City_County")
                        .IsRequired();

                    b.Navigation("County");
                });

            modelBuilder.Entity("Prolunteer.Entities.Event", b =>
                {
                    b.HasOne("Prolunteer.Entities.City", "City")
                        .WithMany("Events")
                        .HasForeignKey("CityId")
                        .HasConstraintName("FK_Event_City")
                        .IsRequired();

                    b.HasOne("Prolunteer.Entities.EventType", "EventType")
                        .WithMany("Events")
                        .HasForeignKey("EventTypeId")
                        .HasConstraintName("FK_Event_EventType")
                        .IsRequired();

                    b.HasOne("Prolunteer.Entities.User", "Organizer")
                        .WithMany("Events")
                        .HasForeignKey("OrganizerId")
                        .HasConstraintName("FK_Event_User")
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("EventType");

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("Prolunteer.Entities.RequiredCertification", b =>
                {
                    b.HasOne("Prolunteer.Entities.Certification", "Certification")
                        .WithMany("RequiredCertifications")
                        .HasForeignKey("CertificationId")
                        .HasConstraintName("FK_RequiredCertification_Certification")
                        .IsRequired();

                    b.HasOne("Prolunteer.Entities.VolunteerPosition", "VolunteerPosition")
                        .WithMany("RequiredCertifications")
                        .HasForeignKey("VolunteerPositionId")
                        .HasConstraintName("FK_RequiredCertification_VolunteerPosition")
                        .IsRequired();

                    b.Navigation("Certification");

                    b.Navigation("VolunteerPosition");
                });

            modelBuilder.Entity("Prolunteer.Entities.UserCertification", b =>
                {
                    b.HasOne("Prolunteer.Entities.Certification", "Certification")
                        .WithMany("UserCertifications")
                        .HasForeignKey("CertificationId")
                        .HasConstraintName("FK_UserCertification_Certification")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prolunteer.Entities.User", "User")
                        .WithMany("UserCertifications")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserCertification_User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Certification");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Prolunteer.Entities.UserCertificationDocument", b =>
                {
                    b.HasOne("Prolunteer.Entities.UserCertification", "UserCertification")
                        .WithMany("UserCertificationDocuments")
                        .HasForeignKey("UserId", "CertificationId")
                        .HasConstraintName("FK_UserCertificationDocument_UserCertification")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserCertification");
                });

            modelBuilder.Entity("Prolunteer.Entities.UserRole", b =>
                {
                    b.HasOne("Prolunteer.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_UserRole_Role")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prolunteer.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserRole_User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Prolunteer.Entities.VolunteerParticipation", b =>
                {
                    b.HasOne("Prolunteer.Entities.User", "User")
                        .WithMany("VolunteerParticipations")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_VolunteerParticipation_User")
                        .IsRequired();

                    b.HasOne("Prolunteer.Entities.VolunteerPosition", "VolunteerPosition")
                        .WithMany("VolunteerParticipations")
                        .HasForeignKey("VolunteerPositionId")
                        .HasConstraintName("FK_VolunteerParticipation_VolunteerPosition")
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("VolunteerPosition");
                });

            modelBuilder.Entity("Prolunteer.Entities.VolunteerPosition", b =>
                {
                    b.HasOne("Prolunteer.Entities.Event", "Event")
                        .WithMany("VolunteerPositions")
                        .HasForeignKey("EventId")
                        .HasConstraintName("FK_VolunteerPosition_Event")
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Prolunteer.Entities.Certification", b =>
                {
                    b.Navigation("RequiredCertifications");

                    b.Navigation("UserCertifications");
                });

            modelBuilder.Entity("Prolunteer.Entities.City", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("Prolunteer.Entities.County", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("Prolunteer.Entities.Event", b =>
                {
                    b.Navigation("VolunteerPositions");
                });

            modelBuilder.Entity("Prolunteer.Entities.EventType", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("Prolunteer.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Prolunteer.Entities.User", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("UserCertifications");

                    b.Navigation("UserRoles");

                    b.Navigation("VolunteerParticipations");
                });

            modelBuilder.Entity("Prolunteer.Entities.UserCertification", b =>
                {
                    b.Navigation("UserCertificationDocuments");
                });

            modelBuilder.Entity("Prolunteer.Entities.VolunteerPosition", b =>
                {
                    b.Navigation("RequiredCertifications");

                    b.Navigation("VolunteerParticipations");
                });
#pragma warning restore 612, 618
        }
    }
}
