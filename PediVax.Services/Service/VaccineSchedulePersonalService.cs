//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using PediVax.BusinessObjects.DTO.VaccineSchedulePersonalDTO;
//using PediVax.BusinessObjects.Enum;
//using PediVax.BusinessObjects.Models;
//using PediVax.Repositories.IRepository;
//using PediVax.Repositories.Repository;
//using PediVax.Services.IService;
//using System.Security.Claims;

//namespace PediVax.Services.Service
//{
//    public class VaccineSchedulePersonalService : IVaccineSchedulePersonalService
//    {
//        private readonly IVaccineSchedulePersonalRepository _vaccineSchedulePersonalRepository;
//        private readonly IVaccineScheduleRepository _vaccineScheduleRepository;
//        private readonly IVaccineProfileRepository _vaccineProfileRepository;
//        private readonly IChildProfileRepository _childProfileRepository;
//        private readonly IMapper _mapper;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly ILogger<VaccineSchedulePersonalService> _logger;

//        public VaccineSchedulePersonalService(
//            IVaccineSchedulePersonalRepository vaccineSchedulePersonalRepository,
//            IVaccineScheduleRepository vaccineScheduleRepository,
//            IChildProfileRepository childProfileRepository,
//            IMapper mapper,
//            IHttpContextAccessor httpContextAccessor,
//            ILogger<VaccineSchedulePersonalService> logger,
//            IVaccineProfileRepository vaccineProfileRepository)
//        {
//            _vaccineSchedulePersonalRepository = vaccineSchedulePersonalRepository;
//            _vaccineScheduleRepository = vaccineScheduleRepository;
//            _childProfileRepository = childProfileRepository;
//            _mapper = mapper;
//            _httpContextAccessor = httpContextAccessor;
//            _logger = logger;
//            _vaccineProfileRepository = vaccineProfileRepository;
//        }

//        private string GetCurrentUserName()
//        {
//            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "System";
//        }

//        private void SetAuditFields(VaccineSchedulePersonal vaccineSchedulePersonal)
//        {
//            vaccineSchedulePersonal.ModifiedBy = GetCurrentUserName();
//            vaccineSchedulePersonal.ModifiedDate = DateTime.UtcNow;
//        }

//        public async Task<List<VaccineSchedulePersonalResponseDTO>> GetAllVaccineSchedulePersonal(CancellationToken cancellationToken)
//        {
//            var vaccineSchedulePersonals = await _vaccineSchedulePersonalRepository.GetAllVaccineSchedulePersonal(cancellationToken);
//            return _mapper.Map<List<VaccineSchedulePersonalResponseDTO>>(vaccineSchedulePersonals);
//        }

//        public async Task<List<VaccineSchedulePersonalResponseDTO>> GetVaccineSchedulePersonalByChildId(int childId, CancellationToken cancellationToken)
//        {
//            var schedules = await _vaccineSchedulePersonalRepository.GetVaccineSchedulePersonalByChildId(childId, cancellationToken);
//            return _mapper.Map<List<VaccineSchedulePersonalResponseDTO>>(schedules);
//        }

//        public async Task<List<VaccineSchedulePersonalResponseDTO>> GenerateVaccineScheduleForChild(int childId, CancellationToken cancellationToken)
//        {
//            var childProfile = await _childProfileRepository.GetChildProfileById(childId);
//            if (childProfile == null)
//            {
//                throw new KeyNotFoundException("Child profile not found.");
//            }

//            DateTime birthDate = childProfile.DateOfBirth;

//            var completedDiseases = await _vaccineProfileRepository.GetCompletedDiseasesForChild(childId, cancellationToken);

//            var vaccineSchedules = await _vaccineScheduleRepository.GetAllVaccineSchedule(cancellationToken);

//            var filteredSchedules = vaccineSchedules
//                .Where(vs => !completedDiseases.Contains(vs.DiseaseId))
//                .ToList();

//            if (!filteredSchedules.Any())
//            {
//                throw new InvalidOperationException("No vaccine schedules available after filtering completed diseases.");
//            }

//            var vaccineSchedulePersonals = new List<VaccineSchedulePersonal>();

//            foreach (var schedule in filteredSchedules)
//            {
//                DateTime scheduledDate = birthDate.AddMonths(schedule.AgeInMonths);

//                var vaccineSchedulePersonal = new VaccineSchedulePersonal
//                {
//                    VaccineScheduleId = schedule.VaccineScheduleId,
//                    ChildId = childId,
//                    DiseaseId = schedule.DiseaseId,
//                    VaccineId = schedule.VaccineId,
//                    DoseNumber = schedule.DoseNumber,
//                    ScheduledDate = scheduledDate,
//                    IsCompleted = EnumList.IsCompleted.No,
//                    IsActive = EnumList.IsActive.Active,
//                    CreatedBy = GetCurrentUserName(),
//                    CreatedDate = DateTime.UtcNow,
//                    ModifiedBy = GetCurrentUserName(),
//                    ModifiedDate = DateTime.UtcNow
//                };

//                vaccineSchedulePersonals.Add(vaccineSchedulePersonal);
//            }

//            await _vaccineSchedulePersonalRepository.AddVaccineSchedulePersonals(vaccineSchedulePersonals, cancellationToken);
//            return _mapper.Map<List<VaccineSchedulePersonalResponseDTO>>(vaccineSchedulePersonals);
//        }

//        public async Task<bool> DeleteVaccineSchedulePersonal(int vaccineSchedulePersonalId, CancellationToken cancellationToken)
//        {
//            if (vaccineSchedulePersonalId <= 0)
//            {
//                throw new ArgumentException("Invalid vaccine schedule personal ID");
//            }

//            var vaccineSchedulePersonal = await _vaccineSchedulePersonalRepository.GetVaccineSchedulePersonalById(vaccineSchedulePersonalId, cancellationToken);
//            if (vaccineSchedulePersonal == null)
//            {
//                _logger.LogWarning("Vaccine schedule personal not found: {vaccineSchedulePersonalId}", vaccineSchedulePersonalId);
//                throw new KeyNotFoundException("Vaccine schedule personal not found");
//            }

//            return await _vaccineSchedulePersonalRepository.DeleteVaccineSchedulePersonal(vaccineSchedulePersonalId, cancellationToken);
//        }
//    }
//}
