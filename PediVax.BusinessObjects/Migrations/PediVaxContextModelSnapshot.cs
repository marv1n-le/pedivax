﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PediVax.BusinessObjects.DBContext;

#nullable disable

namespace PediVax.BusinessObjects.Migrations
{
    [DbContext(typeof(PediVaxContext))]
    partial class PediVaxContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PediVax.BusinessObjects.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("AppointmentStatus")
                        .HasColumnType("int");

                    b.Property<int>("ChildId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.Property<int>("VaccineId")
                        .HasColumnType("int");

                    b.Property<int>("VaccinePackageId")
                        .HasColumnType("int");

                    b.HasKey("AppointmentId");

                    b.HasIndex("ChildId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("VaccineId");

                    b.HasIndex("VaccinePackageId");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.ChildProfile", b =>
                {
                    b.Property<int>("ChildId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChildId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Relationship")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ChildId");

                    b.HasIndex("UserId");

                    b.ToTable("ChildProfile");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.Disease", b =>
                {
                    b.Property<int>("DiseaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiseaseId"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DiseaseId");

                    b.ToTable("Disease");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("VaccineId")
                        .HasColumnType("int");

                    b.Property<int>("VaccinePackageId")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.HasIndex("VaccineId");

                    b.HasIndex("VaccinePackageId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.PaymentDetail", b =>
                {
                    b.Property<int>("PaymentDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentDetailId"));

                    b.Property<DateTime>("AdministeredDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IsCompleted")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.Property<int>("VaccinePackageId")
                        .HasColumnType("int");

                    b.HasKey("PaymentDetailId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("VaccinePackageId");

                    b.ToTable("PaymentDetail");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccinationRecord", b =>
                {
                    b.Property<int>("RecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecordId"));

                    b.Property<DateTime>("AdministeredDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reaction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RecordId");

                    b.HasIndex("AppointmentId");

                    b.ToTable("VaccinationRecord");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.Vaccine", b =>
                {
                    b.Property<int>("VaccineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VaccineId"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfManufacture")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("VaccineId");

                    b.ToTable("Vaccine");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccineDisease", b =>
                {
                    b.Property<int>("VaccineDiseaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VaccineDiseaseId"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DiseaseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("VaccineId")
                        .HasColumnType("int");

                    b.HasKey("VaccineDiseaseId");

                    b.HasIndex("DiseaseId");

                    b.HasIndex("VaccineId");

                    b.ToTable("VaccineDisease");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccineDose", b =>
                {
                    b.Property<int>("DoseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoseId"));

                    b.Property<string>("AgeRange")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoseNumber")
                        .HasColumnType("int");

                    b.Property<string>("IsActive")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VaccineId")
                        .HasColumnType("int");

                    b.HasKey("DoseId");

                    b.HasIndex("VaccineId");

                    b.ToTable("VaccineDose");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccinePackage", b =>
                {
                    b.Property<int>("PackageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PackageId"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PackageId");

                    b.ToTable("VaccinePackage");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccinePackageDetail", b =>
                {
                    b.Property<int>("PackageDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PackageDetailId"));

                    b.Property<string>("IsActive")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("VaccineId")
                        .HasColumnType("int");

                    b.HasKey("PackageDetailId");

                    b.HasIndex("PackageId");

                    b.HasIndex("VaccineId");

                    b.ToTable("VaccinePackageDetail");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccineProfile", b =>
                {
                    b.Property<int>("VaccineProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VaccineProfileId"));

                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<int>("ChildId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("IsCompleted")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reaction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("VaccinationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("VaccineProfileId");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("ChildId");

                    b.ToTable("VaccineProfile");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccineProfileDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoseNumber")
                        .HasColumnType("int");

                    b.Property<int>("VaccineId")
                        .HasColumnType("int");

                    b.Property<int>("VaccineProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VaccineId");

                    b.HasIndex("VaccineProfileId");

                    b.ToTable("VaccineProfileDetail");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.Appointment", b =>
                {
                    b.HasOne("PediVax.BusinessObjects.Models.ChildProfile", "ChildProfile")
                        .WithMany("Appointments")
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PediVax.BusinessObjects.Models.Payment", "Payment")
                        .WithMany("Appointments")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PediVax.BusinessObjects.Models.Vaccine", "Vaccine")
                        .WithMany("Appointments")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PediVax.BusinessObjects.Models.VaccinePackage", "VaccinePackage")
                        .WithMany("Appointments")
                        .HasForeignKey("VaccinePackageId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ChildProfile");

                    b.Navigation("Payment");

                    b.Navigation("Vaccine");

                    b.Navigation("VaccinePackage");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.ChildProfile", b =>
                {
                    b.HasOne("PediVax.BusinessObjects.Models.User", "User")
                        .WithMany("ChildProfiles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.Payment", b =>
                {
                    b.HasOne("PediVax.BusinessObjects.Models.Vaccine", "Vaccine")
                        .WithMany("Payments")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PediVax.BusinessObjects.Models.VaccinePackage", "VaccinePackage")
                        .WithMany("Payments")
                        .HasForeignKey("VaccinePackageId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Vaccine");

                    b.Navigation("VaccinePackage");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.PaymentDetail", b =>
                {
                    b.HasOne("PediVax.BusinessObjects.Models.Payment", "Payment")
                        .WithMany("PaymentDetails")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PediVax.BusinessObjects.Models.VaccinePackage", "VaccinePackage")
                        .WithMany("PaymentDetails")
                        .HasForeignKey("VaccinePackageId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Payment");

                    b.Navigation("VaccinePackage");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccinationRecord", b =>
                {
                    b.HasOne("PediVax.BusinessObjects.Models.Appointment", "Appointment")
                        .WithMany("VaccinationRecords")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccineDisease", b =>
                {
                    b.HasOne("PediVax.BusinessObjects.Models.Disease", "Disease")
                        .WithMany("VaccineDiseases")
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PediVax.BusinessObjects.Models.Vaccine", "Vaccine")
                        .WithMany("VaccineDiseases")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Disease");

                    b.Navigation("Vaccine");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccineDose", b =>
                {
                    b.HasOne("PediVax.BusinessObjects.Models.Vaccine", "Vaccine")
                        .WithMany("VaccineDoses")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Vaccine");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccinePackageDetail", b =>
                {
                    b.HasOne("PediVax.BusinessObjects.Models.VaccinePackage", "VaccinePackage")
                        .WithMany("VaccinePackageDetails")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PediVax.BusinessObjects.Models.Vaccine", "Vaccine")
                        .WithMany("VaccinePackageDetails")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Vaccine");

                    b.Navigation("VaccinePackage");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccineProfile", b =>
                {
                    b.HasOne("PediVax.BusinessObjects.Models.Appointment", "Appointment")
                        .WithMany("VaccineProfiles")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PediVax.BusinessObjects.Models.ChildProfile", "ChildProfile")
                        .WithMany("VaccineProfiles")
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("ChildProfile");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccineProfileDetail", b =>
                {
                    b.HasOne("PediVax.BusinessObjects.Models.Vaccine", "Vaccine")
                        .WithMany("VaccineProfileDetails")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PediVax.BusinessObjects.Models.VaccineProfile", "VaccineProfile")
                        .WithMany("VaccineProfileDetails")
                        .HasForeignKey("VaccineProfileId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Vaccine");

                    b.Navigation("VaccineProfile");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.Appointment", b =>
                {
                    b.Navigation("VaccinationRecords");

                    b.Navigation("VaccineProfiles");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.ChildProfile", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("VaccineProfiles");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.Disease", b =>
                {
                    b.Navigation("VaccineDiseases");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.Payment", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("PaymentDetails");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.User", b =>
                {
                    b.Navigation("ChildProfiles");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.Vaccine", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Payments");

                    b.Navigation("VaccineDiseases");

                    b.Navigation("VaccineDoses");

                    b.Navigation("VaccinePackageDetails");

                    b.Navigation("VaccineProfileDetails");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccinePackage", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("PaymentDetails");

                    b.Navigation("Payments");

                    b.Navigation("VaccinePackageDetails");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccineProfile", b =>
                {
                    b.Navigation("VaccineProfileDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
