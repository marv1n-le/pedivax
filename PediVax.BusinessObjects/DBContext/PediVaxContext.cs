using Microsoft.EntityFrameworkCore;
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
        public PediVaxContext(DbContextOptions<PediVaxContext> options) : base(options) { }

        // DbSet properties
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ChildProfile> ChildProfiles { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VaccinationRecord> VaccinationRecords { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<VaccinePackage> VaccinePackages { get; set; }
        public DbSet<VaccinePackageDetail> VaccinePackageDetails { get; set; }
        public DbSet<VaccineDisease> VaccineDiseases { get; set; }
        public DbSet<VaccineDose> VaccineDoses { get; set; }
        public DbSet<VaccineProfile> VaccineProfiles { get; set; }
        public DbSet<VaccineProfileDetail> VaccineProfileDetails { get; set; }

        // OnModelCreating method to configure relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Appointment -> VaccineProfile relationship (one-to-one or one-to-many)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.VaccineProfile)
                .WithMany(vp => vp.Appointments)
                .HasForeignKey(a => a.VaccineProfileId)
                .OnDelete(DeleteBehavior.SetNull);  // Use DeleteBehavior.Restrict or DeleteBehavior.Cascade if necessary

            // VaccineProfile -> VaccineProfileDetails (one-to-many)
            modelBuilder.Entity<VaccineProfile>()
                .HasMany(vp => vp.VaccineProfileDetails)
                .WithOne(vpd => vpd.VaccineProfile)
                .HasForeignKey(vpd => vpd.VaccineProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            //// VaccineDisease relationship (many-to-many)
            //modelBuilder.Entity<VaccineDisease>()
            //    .HasKey(vd => new { vd.VaccineId, vd.DiseaseId });
            //modelBuilder.Entity<VaccineDisease>()
            //    .HasOne(vd => vd.Vaccine)
            //    .WithMany(v => v.VaccineDisease)
            //    .HasForeignKey(vd => vd.VaccineId);
            //modelBuilder.Entity<VaccineDisease>()
            //    .HasOne(vd => vd.Disease)
            //    .WithMany(d => d.VaccineDiseases)
            //    .HasForeignKey(vd => vd.DiseaseId);

            //// VaccineProfile -> Vaccine relationship (one-to-many)
            //modelBuilder.Entity<VaccineProfile>()
            //    .HasOne(vp => vp.Vaccine)
            //    .WithMany(v => v.)
            //    .HasForeignKey(vp => vp.VaccineId)
            //    .OnDelete(DeleteBehavior.Restrict); // Adjust delete behavior as needed

            //// VaccineDose -> Vaccine relationship (one-to-many)
            //modelBuilder.Entity<VaccineDose>()
            //    .HasOne(vd => vd.Vaccine)
            //    .WithMany(v => v.VaccineDoses)
            //    .HasForeignKey(vd => vd.VaccineId)
            //    .OnDelete(DeleteBehavior.Restrict); // Adjust delete behavior as needed
        }
    }
    }
