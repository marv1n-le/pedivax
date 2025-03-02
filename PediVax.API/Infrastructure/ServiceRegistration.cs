using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository;
using PediVax.Services.Configuration.Mapper;
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
            //Register services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDiseaseService, DiseaseService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAuthService, AuthService>();
            
            //External services
            
            #endregion

            return services;
        }
    }
}
