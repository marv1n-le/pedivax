using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository;
using PediVax.Services.Configuration.Mapper;
using PediVax.Services.ExternalService;
using PediVax.Services.IService;
using PediVax.Services.Service;

namespace PediVax.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            // Configure AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(typeof(MapperEntities).Assembly);
           
            
            #region DependencyInjection
            //Register repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDiseaseRepository, DiseaseRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IChildProfileRepository, ChildProfileRepository>();
            services.AddScoped<IVaccineService, VaccineService>();
            services.AddScoped<IVaccinationRecordService, VaccinationRecordService>();
            services.AddScoped<IVaccinePackageService, VaccinePackageService>();
            services.AddScoped<IVaccinePackageDetailService, VaccinePackageDetailService>();
            services.AddScoped<IVaccineScheduleService, VaccineScheduleService>();
            services.AddScoped<IVaccineProfileService, VaccineProfileService>();


            //Register services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDiseaseService, DiseaseService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IChildProfileService, ChildProfileService>();
            services.AddScoped<IVaccineRepository, VaccineRepository>();
            services.AddScoped<IVaccinationRecordRepository, VaccinationRecordRepository>();
            services.AddScoped<IVaccinePackageRepository, VaccinePackageRepository>();
            services.AddScoped<IVaccinePackageDetailRepository, VaccinePackageDetailRepository>();
            services.AddScoped<IVaccineScheduleRepository, VaccineScheduleRepository>();
            services.AddScoped<IVaccineProfileRepository, VaccineProfileRepository>();

            //External services

            #endregion

            return services;
        }
    }
}
