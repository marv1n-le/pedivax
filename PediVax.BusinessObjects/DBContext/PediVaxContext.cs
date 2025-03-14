using Microsoft.EntityFrameworkCore;
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
                // Đọc appsettings.json từ thư mục hiện tại
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables() // Thêm dòng này để đọc từ biến môi trường Azure
                    .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrEmpty(connectionString))
                {
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
        public DbSet<VaccineProfile> VaccineProfiles { get; set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships using Fluent API
            modelBuilder.Entity<User>()
                .HasMany(u => u.ChildProfiles)
                .WithOne(cp => cp.User)
                .HasForeignKey(cp => cp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Payments)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

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
                .HasForeignKey(vpd => vpd.VaccinePackageId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VaccinePackageDetail>()
                .HasOne(vpd => vpd.Vaccine)
                .WithMany(v => v.VaccinePackageDetails)
                .HasForeignKey(vpd => vpd.VaccineId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.PaymentDetail)
                .WithMany(pd => pd.Appointments)
                .HasForeignKey(a => a.PaymentDetailId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasMany(p => p.PaymentDetails)
                .WithOne(pd => pd.Payment)
                .HasForeignKey(pd => pd.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);

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
                .HasOne(a => a.User)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.UserId)
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

            modelBuilder.Entity<VaccineProfile>()
                .HasOne(vsp => vsp.VaccineSchedule)
                .WithMany(cp => cp.VaccineProfiles)
                .HasForeignKey(vsp => vsp.VaccineScheduleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PaymentDetail>()
                .HasOne(pd => pd.Payment)
                .WithMany(p => p.PaymentDetails)
                .HasForeignKey(pd => pd.PaymentId)
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

            SeedData.Seed(modelBuilder);
        }
    }
}
