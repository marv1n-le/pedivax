using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository.BaseRepository;
using System.Threading;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PediVax.BusinessObjects.Enum;

namespace PediVax.Repositories.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository() : base()
        {
        }

        public async Task<List<Appointment>> GetAllAppointments(CancellationToken cancellationToken)
        {
            return await _context.Appointments
                .Include(a => a.Vaccine)          // Load dữ liệu Vaccine
                .Include(a => a.VaccinePackage)   // Load dữ liệu VaccinePackage
                .ToListAsync(cancellationToken);
        }


        public async Task<(List<Appointment> Data, int TotalCount)> GetAppointmentsPaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await GetPagedAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task<Appointment> GetAppointmentById(int appointmentId, CancellationToken cancellationToken)
        {
            return await _context.Appointments
                .Include(a => a.Vaccine)          // Load dữ liệu Vaccine
                .Include(a => a.VaccinePackage)   // Load dữ liệu VaccinePackage
                .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId, cancellationToken);
        }

        public async Task<List<Appointment>> GetAppointmentsByChildId(int childId, CancellationToken cancellationToken)
        {
            return await _context.Appointments
                .Where(a => a.ChildId == childId)
                .Include(a => a.Vaccine)
                .Include(a => a.VaccinePackage)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Appointment>> GetAppointmentsByDate(DateTime appointmentDate, CancellationToken cancellationToken)
        {
            return await _context.Appointments
                .Where(a => a.AppointmentDate.Date == appointmentDate.Date)
                .Include(a => a.Vaccine)
                .Include(a => a.VaccinePackage)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Appointment>> GetAppointmentsByStatus(EnumList.AppointmentStatus status, CancellationToken cancellationToken)
        {
            return await _context.Appointments
                .Where(a => a.AppointmentStatus == status)
                .Include(a => a.Vaccine)          // Load dữ liệu Vaccine
                .Include(a => a.VaccinePackage)   // Load dữ liệu VaccinePackage
                .ToListAsync(cancellationToken);
        }



        public async Task<int> AddAppointment(Appointment appointment, CancellationToken cancellationToken)
        {
            return await CreateAsync(appointment, cancellationToken);
        }

        public async Task<int> UpdateAppointment(Appointment appointment, CancellationToken cancellationToken)
        {
            return await UpdateAsync(appointment, cancellationToken);
        }

        public async Task<bool> DeleteAppointment(int appointmentId, CancellationToken cancellationToken)
        {
            return await DeleteAsync(appointmentId, cancellationToken);
        }

        public async Task<int> GetQuantityAppointmentByPackageIdAndVaccineId(int childId, int packageId, int vaccineId, CancellationToken cancellationToken)
        {
            return await _context.Appointments
                .Where(a => a.ChildId == childId && a.VaccinePackageId == packageId && a.VaccineId == vaccineId)
                .CountAsync();
        }
    }
}
