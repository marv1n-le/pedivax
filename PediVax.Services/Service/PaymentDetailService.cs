using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.PaymentDetailDTO;
using PediVax.BusinessObjects.DTO.VaccineProfileDTO;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PediVax.Services.IService;
using PediVax.Repositories.Repository;

namespace PediVax.Services.Service
{
    public class PaymentDetailService : IPaymentDetailService

    {
        private readonly IPaymentDetailRepository _paymentDetailRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IVaccinePackageDetailRepository _vaccinePackageDetailRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentDetailService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaymentDetailService(
            IPaymentDetailRepository paymentDetailRepository,
            IPaymentRepository paymentRepository,
            IVaccinePackageDetailRepository vaccinePackageDetailRepository,
            IMapper mapper,
            ILogger<PaymentDetailService> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _paymentDetailRepository = paymentDetailRepository;
            _paymentRepository = paymentRepository;
            _vaccinePackageDetailRepository = vaccinePackageDetailRepository;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetCurrentUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "System";
        }

        public async Task<List<PaymentDetailResponseDTO>> GetAllPaymentDetails(CancellationToken cancellationToken)
        {
            var paymentDetails = await _paymentDetailRepository.GetAllPaymentDetails(cancellationToken);
            return _mapper.Map<List<PaymentDetailResponseDTO>>(paymentDetails);
        }

        public async Task<PaymentDetailResponseDTO> GetPaymentDetailById(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid payment detail ID: {id}", id);
                throw new ArgumentException("Invalid payment detail ID");
            }

            var paymentDetail = await _paymentDetailRepository.GetPaymentDetailById(id, cancellationToken);
            if (paymentDetail == null)
            {
                _logger.LogWarning("Payment detail not found for ID: {id}", id);
                throw new KeyNotFoundException("Payment detail not found");
            }

            return _mapper.Map<PaymentDetailResponseDTO>(paymentDetail);
        }

        public async Task<List<PaymentDetailResponseDTO>> GeneratePaymentDetails(int paymentId, CancellationToken cancellationToken)
        {
            if (paymentId <= 0)
            {
                _logger.LogWarning("Invalid payment ID: {paymentId}", paymentId);
                throw new ArgumentException("Invalid payment ID");
            }

            var payment = await _paymentRepository.GetPaymentById(paymentId, cancellationToken);
            if (payment == null)
            {
                throw new KeyNotFoundException("Payment not found");
            }

            if (payment.PaymentStatus != EnumList.PaymentStatus.Paid)
            {
                throw new InvalidOperationException("Payment is not completed or already processed.");
            }

            var vaccinePackageDetails = await _vaccinePackageDetailRepository.GetVaccinePackageDetailByPackageId(payment.VaccinePackageId.Value, cancellationToken);
            if (!vaccinePackageDetails.Any())
            {
                throw new InvalidOperationException("No vaccine package details found for the given package.");
            }

            var existingPaymentDetails = await _paymentDetailRepository.GetPaymentDetailByPaymentId(paymentId, cancellationToken);
            if (existingPaymentDetails.Any())
            {
                return _mapper.Map<List<PaymentDetailResponseDTO>>(existingPaymentDetails);
            }

            DateTime scheduledDate = DateTime.UtcNow;
            var paymentDetailsToCreate = new List<PaymentDetail>();
            int doseSequenceCounter = 1;

            foreach (var vaccinePackageDetail in vaccinePackageDetails)
            {
                var paymentDetail = new PaymentDetail
                {
                    PaymentId = paymentId,
                    VaccinePackageDetailId = vaccinePackageDetail.VaccinePackageDetailId,
                    IsCompleted = EnumList.IsCompleted.No,
                    DoseSequence = doseSequenceCounter,
                    ScheduledDate = scheduledDate,
                    AdministeredDate = null,
                    Notes = "Pending vaccination",
                };
                paymentDetailsToCreate.Add(paymentDetail);
                scheduledDate = scheduledDate.AddDays(2);
                doseSequenceCounter++;
            }

            if (paymentDetailsToCreate.Any())
            {
                await _paymentDetailRepository.GeneratePaymentDetail(paymentDetailsToCreate, cancellationToken);
            }

            return _mapper.Map<List<PaymentDetailResponseDTO>>(paymentDetailsToCreate);
        }

        public async Task<List<PaymentDetailResponseDTO>> GetPaymentDetailByPaymentId(int paymentId,
            CancellationToken cancellationToken)
        {
            if (paymentId <= 0)
            {
                _logger.LogWarning("Invalid payment ID: {paymentId}", paymentId);
                throw new ArgumentException("Invalid payment ID");
            }

            var paymentDetails = await _paymentDetailRepository.GetPaymentDetailByPaymentId(paymentId, cancellationToken);
            return _mapper.Map<List<PaymentDetailResponseDTO>>(paymentDetails);
        }

        public async Task<List<PaymentDetailResponseDTO>> GetUncompletedByPaymentId(int paymentId,
            CancellationToken cancellationToken)
        {
            if (paymentId <= 0)
            {
                _logger.LogWarning("Invalid payment ID: {paymentId}", paymentId);
                throw new ArgumentException("Invalid payment ID");
            }
            var paymentDetails = await _paymentDetailRepository.GetUncompletedByPaymentId(paymentId, cancellationToken);
            return _mapper.Map<List<PaymentDetailResponseDTO>>(paymentDetails);
        }

        public async Task<bool> UpdatePaymentDetail(int id, UpdatePaymentDetailDTO updatePaymentDetailDTO,
            CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid payment detail ID: {id}", id);
                throw new ArgumentException("Invalid payment detail ID");
            }

            var paymentDetail = await _paymentDetailRepository.GetPaymentDetailById(id, cancellationToken);
            if (paymentDetail == null)
            {
                _logger.LogWarning("PaymentDetail not found for ID: {id}", id);
                throw new KeyNotFoundException("PaymentDetail not found");
            }

            paymentDetail.IsCompleted = (EnumList.IsCompleted) updatePaymentDetailDTO.IsCompleted;
            paymentDetail.AdministeredDate = updatePaymentDetailDTO.AdministeredDate;
            paymentDetail.Notes = updatePaymentDetailDTO.Notes;

            int rowsAffected = await _paymentDetailRepository.UpdatePaymentDetail(paymentDetail, cancellationToken);
            return rowsAffected > 0;
        }
    }
}
