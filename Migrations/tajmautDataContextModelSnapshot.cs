﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tajmautAPI.Data;

#nullable disable

namespace tajmautAPI.Migrations
{
    [DbContext(typeof(tajmautDataContext))]
    partial class tajmautDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TajmautMK.Common.Models.EntityClasses.ForgotPassEntity", b =>
                {
                    b.Property<int>("ForgotPassEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ForgotPassEntityId"));

                    b.Property<DateTime>("Expire")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ForgotPassEntityId");

                    b.HasIndex("UserId");

                    b.ToTable("ForgotPassEntity");
                });

            modelBuilder.Entity("TajmautMK.Common.Models.EntityClasses.Venue_City", b =>
                {
                    b.Property<int>("Venue_CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Venue_CityId"));

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Venue_CityId");

                    b.ToTable("Venue_Cities");
                });

            modelBuilder.Entity("TajmautMK.Common.Models.EntityClasses.Venue_Types", b =>
                {
                    b.Property<int>("Venue_TypesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Venue_TypesId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Venue_TypesId");

                    b.ToTable("VenueTypes");
                });

            modelBuilder.Entity("tajmautAPI.Models.EntityClasses.CategoryEvent", b =>
                {
                    b.Property<int>("CategoryEventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryEventId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryEventId");

                    b.ToTable("CategoryEvents");
                });

            modelBuilder.Entity("tajmautAPI.Models.EntityClasses.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<int>("Review")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VenueId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("UserId");

                    b.HasIndex("VenueId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("tajmautAPI.Models.EntityClasses.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventId"));

                    b.Property<int>("CategoryEventId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("EventImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VenueId")
                        .HasColumnType("int");

                    b.Property<bool>("isCanceled")
                        .HasColumnType("bit");

                    b.HasKey("EventId");

                    b.HasIndex("CategoryEventId");

                    b.HasIndex("VenueId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("tajmautAPI.Models.EntityClasses.OnlineReservation", b =>
                {
                    b.Property<int>("OnlineReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OnlineReservationId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<int>("NumberGuests")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VenueId")
                        .HasColumnType("int");

                    b.HasKey("OnlineReservationId");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.HasIndex("VenueId");

                    b.ToTable("OnlineReservations");
                });

            modelBuilder.Entity("tajmautAPI.Models.EntityClasses.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("tajmautAPI.Models.EntityClasses.Venue", b =>
                {
                    b.Property<int>("VenueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VenueId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VenueTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Venue_CityId")
                        .HasColumnType("int");

                    b.HasKey("VenueId");

                    b.HasIndex("VenueTypeId");

                    b.HasIndex("Venue_CityId");

                    b.ToTable("Venues");
                });

            modelBuilder.Entity("TajmautMK.Common.Models.EntityClasses.ForgotPassEntity", b =>
                {
                    b.HasOne("tajmautAPI.Models.EntityClasses.User", "user")
                        .WithMany("ForgotPassChanges")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("tajmautAPI.Models.EntityClasses.Comment", b =>
                {
                    b.HasOne("tajmautAPI.Models.EntityClasses.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tajmautAPI.Models.EntityClasses.Venue", "Venue")
                        .WithMany("Comments")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("tajmautAPI.Models.EntityClasses.Event", b =>
                {
                    b.HasOne("tajmautAPI.Models.EntityClasses.CategoryEvent", "CategoryEvent")
                        .WithMany("Events")
                        .HasForeignKey("CategoryEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tajmautAPI.Models.EntityClasses.Venue", "Venue")
                        .WithMany("Events")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryEvent");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("tajmautAPI.Models.EntityClasses.OnlineReservation", b =>
                {
                    b.HasOne("tajmautAPI.Models.EntityClasses.Event", "Event")
                        .WithMany("OnlineReservations")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tajmautAPI.Models.EntityClasses.User", "User")
                        .WithMany("OnlineReservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tajmautAPI.Models.EntityClasses.Venue", "Venue")
                        .WithMany("OnlineReservations")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("tajmautAPI.Models.EntityClasses.Venue", b =>
                {
                    b.HasOne("TajmautMK.Common.Models.EntityClasses.Venue_Types", "VenueType")
                        .WithMany("Venues")
                        .HasForeignKey("VenueTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TajmautMK.Common.Models.EntityClasses.Venue_City", "Venue_City")
                        .WithMany("Venues")
                        .HasForeignKey("Venue_CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VenueType");

                    b.Navigation("Venue_City");
                });

            modelBuilder.Entity("TajmautMK.Common.Models.EntityClasses.Venue_City", b =>
                {
                    b.Navigation("Venues");
                });

            modelBuilder.Entity("TajmautMK.Common.Models.EntityClasses.Venue_Types", b =>
                {
                    b.Navigation("Venues");
                });

            modelBuilder.Entity("tajmautAPI.Models.EntityClasses.CategoryEvent", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("tajmautAPI.Models.EntityClasses.Event", b =>
                {
                    b.Navigation("OnlineReservations");
                });

            modelBuilder.Entity("tajmautAPI.Models.EntityClasses.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("ForgotPassChanges");

                    b.Navigation("OnlineReservations");
                });

            modelBuilder.Entity("tajmautAPI.Models.EntityClasses.Venue", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Events");

                    b.Navigation("OnlineReservations");
                });
#pragma warning restore 612, 618
        }
    }
}
