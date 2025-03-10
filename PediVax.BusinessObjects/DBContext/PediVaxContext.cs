﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PediVax.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DBContext
{
    public class PediVaxContext : DbContext
    {
        public PediVaxContext() { }
        public PediVaxContext(DbContextOptions<PediVaxContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "PediVax.API"); // Trỏ về thư mục chứa appsettings.json
                Console.WriteLine($"Adjusted Base Path: {path}");

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection");
                Console.WriteLine($"Connection String: {connectionString}");

                if (string.IsNullOrEmpty(connectionString))
                {
                    Console.WriteLine("⚠️ ConnectionString vẫn null! Kiểm tra đường dẫn appsettings.json");
                    throw new InvalidOperationException("Không thể lấy ConnectionString.");
                }

                optionsBuilder.UseSqlServer(connectionString);
            }
        }


        // DbSet properties
        public DbSet<User> Users { get; set; }
        public DbSet<ChildProfile> ChildProfiles { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<VaccineSchedule> VaccineSchedules { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<VaccineDisease> VaccineDiseases { get; set; }
        public DbSet<VaccinePackage> VaccinePackages { get; set; }
        public DbSet<VaccinePackageDetail> VaccinePackageDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<VaccinationRecord> VaccinationRecords { get; set; }
        public DbSet<VaccineProfile> VaccineProfiles { get; set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<VaccineSchedulePersonal> VaccineSchedulePersonals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships using Fluent API
            modelBuilder.Entity<User>()
                .HasMany(u => u.ChildProfiles)
                .WithOne(cp => cp.User)
                .HasForeignKey(cp => cp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<ChildProfile>()
                .HasOne(cp => cp.User)
                .WithMany(u => u.ChildProfiles)
                .HasForeignKey(cp => cp.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VaccineSchedule>()
                .HasOne(vs => vs.Disease)
                .WithMany(d => d.VaccineSchedules)
                .HasForeignKey(vs => vs.DiseaseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VaccineSchedule>()
                .HasOne(vs => vs.Vaccine)
                .WithMany(v => v.VaccineSchedules)
                .HasForeignKey(vs => vs.VaccineId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<VaccineDisease>()
                .HasOne(vd => vd.Vaccine)
                .WithMany(v => v.VaccineDiseases)
                .HasForeignKey(vd => vd.VaccineId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VaccineDisease>()
                .HasOne(vd => vd.Disease)
                .WithMany(d => d.VaccineDiseases)
                .HasForeignKey(vd => vd.DiseaseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VaccinePackageDetail>()
                .HasOne(vpd => vpd.VaccinePackage)
                .WithMany(vp => vp.VaccinePackageDetails)
                .HasForeignKey(vpd => vpd.PackageId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VaccinePackageDetail>()
                .HasOne(vpd => vpd.Vaccine)
                .WithMany(v => v.VaccinePackageDetails)
                .HasForeignKey(vpd => vpd.VaccineId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.VaccinePackage)
                .WithMany(vp => vp.Payments)
                .HasForeignKey(p => p.VaccinePackageId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Vaccine)
                .WithMany(v => v.Payments)
                .HasForeignKey(p => p.VaccineId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Payment)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PaymentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.ChildProfile)
                .WithMany(cp => cp.Appointments)
                .HasForeignKey(a => a.ChildId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Vaccine)
                .WithMany(v => v.Appointments)
                .HasForeignKey(a => a.VaccineId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.VaccinePackage)
                .WithMany(vp => vp.Appointments)
                .HasForeignKey(a => a.VaccinePackageId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VaccinationRecord>()
                .HasOne(vr => vr.Appointment)
                .WithMany(a => a.VaccinationRecords)
                .HasForeignKey(vr => vr.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VaccineProfile>()
                .HasOne(vp => vp.Appointment)
                .WithMany(a => a.VaccineProfiles)
                .HasForeignKey(vp => vp.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VaccineProfile>()
                .HasOne(vp => vp.ChildProfile)
                .WithMany(cp => cp.VaccineProfiles)
                .HasForeignKey(vp => vp.ChildId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VaccineProfile>()
                .HasOne(vp => vp.Disease)
                .WithMany(d => d.VaccineProfiles)
                .HasForeignKey(vp => vp.DiseaseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VaccineSchedulePersonal>()
                .HasOne(vsp => vsp.ChildProfile)
                .WithMany(cp => cp.VaccineSchedulePersonals)
                .HasForeignKey(vsp => vsp.ChildId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VaccineSchedulePersonal>()
                .HasOne(vsp => vsp.Disease)
                .WithMany(d => d.VaccineSchedulePersonals)
                .HasForeignKey(vsp => vsp.DiseaseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VaccineSchedulePersonal>()
                .HasOne(vsp => vsp.Vaccine)
                .WithMany(v => v.VaccineSchedulePersonals)
                .HasForeignKey(vsp => vsp.VaccineId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PaymentDetail>()
                .HasOne(pd => pd.Payment)
                .WithMany(p => p.PaymentDetails)
                .HasForeignKey(pd => pd.PaymentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PaymentDetail>()
                .HasOne(pd => pd.VaccinePackage)
                .WithMany(vp => vp.PaymentDetails)
                .HasForeignKey(pd => pd.VaccinePackageId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Vaccine>()
                .Property(v => v.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<VaccinePackage>()
                .Property(vp => vp.TotalPrice)
                .HasColumnType("decimal(18,2)");
            
            modelBuilder.Entity<Payment>()
                .Property(p => p.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<VaccineSchedulePersonal>()
                .HasOne(vsp => vsp.VaccineSchedule)
                .WithMany(vs => vs.VaccineSchedulePersonals)
                .HasForeignKey(vsp => vsp.VaccineScheduleId)
                .OnDelete(DeleteBehavior.NoAction);

            SeedData.Seed(modelBuilder);
        }
    }
}
