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
            services.AddScoped<IVaccinePackageService, VaccinePackageService>();
            services.AddScoped<IVaccinePackageDetailService, VaccinePackageDetailService>();
            services.AddScoped<IVaccineScheduleService, VaccineScheduleService>();
            services.AddScoped<IVaccineProfileService, VaccineProfileService>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentDetailRepository, PaymentDetailRepository>();

            //Register services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDiseaseService, DiseaseService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IChildProfileService, ChildProfileService>();
            services.AddScoped<IVaccineRepository, VaccineRepository>();
            services.AddScoped<IVaccinePackageRepository, VaccinePackageRepository>();
            services.AddScoped<IVaccinePackageDetailRepository, VaccinePackageDetailRepository>();
            services.AddScoped<IVaccineScheduleRepository, VaccineScheduleRepository>();
            services.AddScoped<IVaccineProfileRepository, VaccineProfileRepository>();
            services.AddScoped<IVnPayService, VnPayService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentDetailService, PaymentDetailService>();

            //External services

            #endregion

            return services;
        }
    }
}
