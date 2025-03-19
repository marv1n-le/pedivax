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

                    b.Property<int?>("PaymentDetailId")
                        .HasColumnType("int");

                    b.Property<string>("Reaction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("VaccineId")
                        .HasColumnType("int");

                    b.Property<int?>("VaccinePackageId")
                        .HasColumnType("int");

                    b.HasKey("AppointmentId");

                    b.HasIndex("ChildId");

                    b.HasIndex("PaymentDetailId");

                    b.HasIndex("UserId");

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

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

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

                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<string>("PaymentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("VaccineId")
                        .HasColumnType("int");

                    b.Property<int?>("VaccinePackageId")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("UserId");

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

                    b.Property<DateTime?>("AdministeredDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoseSequence")
                        .HasColumnType("int");

                    b.Property<int>("IsCompleted")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ScheduledDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("VaccinePackageDetailId")
                        .HasColumnType("int");

                    b.Property<int?>("VaccinePackageId")
                        .HasColumnType("int");

                    b.HasKey("PaymentDetailId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("VaccinePackageDetailId");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Address = "PediVax HCM",
                            CreatedBy = "System",
                            CreatedDate = new DateTime(2025, 3, 19, 3, 8, 52, 660, DateTimeKind.Utc).AddTicks(6476),
                            DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@pedivax.com",
                            FullName = "System Admin",
                            Image = "https://pedivax.com/images/user.png",
                            IsActive = 1,
                            ModifiedBy = "System",
                            ModifiedDate = new DateTime(2025, 3, 19, 3, 8, 52, 660, DateTimeKind.Utc).AddTicks(6481),
                            PasswordHash = "BYCe27mYnGvCFrjfqSnDtvKdoGs7ojK5bNLMRL+Xdgw=",
                            PasswordSalt = "Tr+ygRhDOm9l6xGzkVJTuPHX3mR5XNIIICB3kPUHevk=",
                            PhoneNumber = "0123456789",
                            Role = 1
                        });
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

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

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

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccinePackage", b =>
                {
                    b.Property<int>("VaccinePackageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VaccinePackageId"));

                    b.Property<int>("AgeInMonths")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
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

                    b.Property<int>("TotalDoses")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("VaccinePackageId");

                    b.ToTable("VaccinePackage");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccinePackageDetail", b =>
                {
                    b.Property<int>("VaccinePackageDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VaccinePackageDetailId"));

                    b.Property<int>("DoseNumber")
                        .HasColumnType("int");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<int>("VaccineId")
                        .HasColumnType("int");

                    b.Property<int>("VaccinePackageId")
                        .HasColumnType("int");

                    b.HasKey("VaccinePackageDetailId");

                    b.HasIndex("VaccineId");

                    b.HasIndex("VaccinePackageId");

                    b.ToTable("VaccinePackageDetail");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccineProfile", b =>
                {
                    b.Property<int>("VaccineProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VaccineProfileId"));

                    b.Property<int?>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<int>("ChildId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DiseaseId")
                        .HasColumnType("int");

                    b.Property<int>("DoseNumber")
                        .HasColumnType("int");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<int>("IsCompleted")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ScheduledDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("VaccinationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("VaccineScheduleId")
                        .HasColumnType("int");

                    b.HasKey("VaccineProfileId");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("ChildId");

                    b.HasIndex("DiseaseId");

                    b.HasIndex("VaccineScheduleId");

                    b.ToTable("VaccineProfile");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccineSchedule", b =>
                {
                    b.Property<int>("VaccineScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VaccineScheduleId"));

                    b.Property<int>("AgeInMonths")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DiseaseId")
                        .HasColumnType("int");

                    b.Property<int>("DoseNumber")
                        .HasColumnType("int");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("VaccineScheduleId");

                    b.HasIndex("DiseaseId");

                    b.ToTable("VaccineSchedule");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.Appointment", b =>
                {
                    b.HasOne("PediVax.BusinessObjects.Models.ChildProfile", "ChildProfile")
                        .WithMany("Appointments")
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PediVax.BusinessObjects.Models.PaymentDetail", "PaymentDetail")
                        .WithMany("Appointments")
                        .HasForeignKey("PaymentDetailId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("PediVax.BusinessObjects.Models.User", "User")
                        .WithMany("Appointments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PediVax.BusinessObjects.Models.Vaccine", "Vaccine")
                        .WithMany("Appointments")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("PediVax.BusinessObjects.Models.VaccinePackage", "VaccinePackage")
                        .WithMany("Appointments")
                        .HasForeignKey("VaccinePackageId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("ChildProfile");

                    b.Navigation("PaymentDetail");

                    b.Navigation("User");

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
                    b.HasOne("PediVax.BusinessObjects.Models.Appointment", "Appointment")
                        .WithMany("Payments")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PediVax.BusinessObjects.Models.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PediVax.BusinessObjects.Models.Vaccine", "Vaccine")
                        .WithMany("Payments")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("PediVax.BusinessObjects.Models.VaccinePackage", "VaccinePackage")
                        .WithMany("Payments")
                        .HasForeignKey("VaccinePackageId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Appointment");

                    b.Navigation("User");

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

                    b.HasOne("PediVax.BusinessObjects.Models.VaccinePackageDetail", "VaccinePackageDetail")
                        .WithMany("PaymentDetails")
                        .HasForeignKey("VaccinePackageDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PediVax.BusinessObjects.Models.VaccinePackage", null)
                        .WithMany("PaymentDetails")
                        .HasForeignKey("VaccinePackageId");

                    b.Navigation("Payment");

                    b.Navigation("VaccinePackageDetail");
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

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccinePackageDetail", b =>
                {
                    b.HasOne("PediVax.BusinessObjects.Models.Vaccine", "Vaccine")
                        .WithMany("VaccinePackageDetails")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PediVax.BusinessObjects.Models.VaccinePackage", "VaccinePackage")
                        .WithMany("VaccinePackageDetails")
                        .HasForeignKey("VaccinePackageId")
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
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("PediVax.BusinessObjects.Models.ChildProfile", "ChildProfile")
                        .WithMany("VaccineProfiles")
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PediVax.BusinessObjects.Models.Disease", "Disease")
                        .WithMany("VaccineProfiles")
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PediVax.BusinessObjects.Models.VaccineSchedule", "VaccineSchedule")
                        .WithMany("VaccineProfiles")
                        .HasForeignKey("VaccineScheduleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("ChildProfile");

                    b.Navigation("Disease");

                    b.Navigation("VaccineSchedule");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccineSchedule", b =>
                {
                    b.HasOne("PediVax.BusinessObjects.Models.Disease", "Disease")
                        .WithMany("VaccineSchedules")
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Disease");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.Appointment", b =>
                {
                    b.Navigation("Payments");

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

                    b.Navigation("VaccineProfiles");

                    b.Navigation("VaccineSchedules");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.Payment", b =>
                {
                    b.Navigation("PaymentDetails");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.PaymentDetail", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.User", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("ChildProfiles");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.Vaccine", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Payments");

                    b.Navigation("VaccineDiseases");

                    b.Navigation("VaccinePackageDetails");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccinePackage", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("PaymentDetails");

                    b.Navigation("Payments");

                    b.Navigation("VaccinePackageDetails");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccinePackageDetail", b =>
                {
                    b.Navigation("PaymentDetails");
                });

            modelBuilder.Entity("PediVax.BusinessObjects.Models.VaccineSchedule", b =>
                {
                    b.Navigation("VaccineProfiles");
                });
#pragma warning restore 612, 618
        }
    }
}
