using System.Security.Claims;
using System.Threading;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.PaymentDTO;
using PediVax.BusinessObjects.DTO.VnPayDTO;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository;
using PediVax.Services.IService;
using static PediVax.BusinessObjects.Enum.EnumList;


namespace PediVax.Services.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<PaymentService> _logger;
        private readonly IVnPayService _vnPayService;
        private readonly IVaccineRepository _vaccineRepository;
        private readonly IVaccinePackageRepository _vaccinePackageRepository;



        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor,
                          ILogger<PaymentService> logger, IVnPayService vnPayService, IVaccineRepository vaccineRepository, IVaccinePackageRepository vaccinePackageRepository)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _vnPayService = vnPayService;

            // Khởi tạo các repository
            _vaccineRepository = vaccineRepository;
            _vaccinePackageRepository = vaccinePackageRepository;
        }

        private string GetCurrentUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "System";
        }

        private void SetAuditFields(Payment payment)
        {
            payment.CreatedBy = GetCurrentUserName();
            payment.CreatedDate = DateTime.UtcNow;
        }

        public async Task<PaymentResponseDTO> GetPaymentById(int paymentId, CancellationToken cancellationToken)
        {
            if (paymentId <= 0)
            {
                _logger.LogWarning("Invalid payment ID: {paymentId}", paymentId);
                throw new ArgumentException("Invalid payment ID");
            }

            var payment = await _paymentRepository.GetPaymentById(paymentId, cancellationToken);
            if (payment == null)
            {
                _logger.LogWarning("Payment with ID {paymentId} not found", paymentId);
                throw new KeyNotFoundException("Payment not found");
            }

            return _mapper.Map<PaymentResponseDTO>(payment);
        }



        public async Task<PaymentResponseDTO> AddPayment(CreatePaymentDTO createPaymentDTO, CancellationToken cancellationToken)
        {
            if (createPaymentDTO == null)
            {
                throw new ArgumentNullException(nameof(createPaymentDTO), "Payment data is required");
            }

            try
            {
                var payment = _mapper.Map<Payment>(createPaymentDTO);

                if (createPaymentDTO.VaccinePackageId.HasValue && createPaymentDTO.VaccineId.HasValue)
                {
                    throw new ApplicationException("Cannot have both VaccinePackageId and VaccineId. Choose only one.");
                }

                if (createPaymentDTO.VaccinePackageId.HasValue)
                {
                    var vaccinePackage = await _vaccinePackageRepository.GetVaccinePackageById(createPaymentDTO.VaccinePackageId.Value, cancellationToken);
                    if (vaccinePackage == null)
                    {
                        throw new KeyNotFoundException("Vaccine package not found");
                    }
                    payment.TotalAmount = vaccinePackage.TotalPrice ?? 0;
                    payment.VaccineId = null;
                }
                else if (createPaymentDTO.VaccineId.HasValue)
                {
                    var vaccine = await _vaccineRepository.GetVaccineById(createPaymentDTO.VaccineId.Value, cancellationToken);
                    if (vaccine == null)
                    {
                        throw new KeyNotFoundException("Vaccine not found");
                    }
                    payment.TotalAmount = vaccine.Price;
                    payment.VaccinePackageId = null; // Đảm bảo VaccinePackageId luôn null
                }
                else
                {
                    throw new ApplicationException("Either VaccineId or VaccinePackageId must be provided.");
                }

                // Đảm bảo TotalAmount hợp lệ
                if (payment.TotalAmount <= 0)
                {
                    throw new ApplicationException("TotalAmount must be greater than 0.");
                }

                // Đảm bảo trạng thái thanh toán
                payment.PaymentStatus = EnumList.PaymentStatus.AwaitingPayment;
                payment.PaymentDate = DateTime.UtcNow;
                SetAuditFields(payment);

                _logger.LogInformation($"Creating Payment: VaccineId={payment.VaccineId}, VaccinePackageId={payment.VaccinePackageId}, TotalAmount={payment.TotalAmount}");

                // Lưu vào database
                var createdPaymentId = await _paymentRepository.CreatePayment(payment, cancellationToken);
                if (createdPaymentId <= 0)
                {
                    throw new ApplicationException("Adding new payment failed.");
                }

                // Lấy lại thông tin từ DB
                var savedPayment = await _paymentRepository.GetPaymentById(createdPaymentId, cancellationToken);
                if (savedPayment == null)
                {
                    throw new ApplicationException("Failed to retrieve the newly created payment.");
                }

                var response = _mapper.Map<PaymentResponseDTO>(savedPayment);

                // ✅ Kiểm tra lại dữ liệu trả về
                response.VaccineId = savedPayment.VaccineId; // Chỉ có nếu chọn vaccine
                response.VaccinePackageId = savedPayment.VaccinePackageId; // Chỉ có nếu chọn vaccine package

                // ✅ Tạo URL thanh toán nếu cần
                var model = new VnPaymentRequestModel()
                {
                    Amount = (double)payment.TotalAmount,
                    FullName = "Khach hang",
                    Description = payment.VaccinePackageId + "  " + payment.VaccineId,
                    OrderId = payment.PaymentId
                };
                _logger.LogInformation("ABC:" + model.OrderId);

                response.URL = _vnPayService.CreatePaymentUrl(_httpContextAccessor.HttpContext, model);
                response.PaymentDate = payment.PaymentDate;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new payment: " + ex.Message);
                throw new ApplicationException($"Error while saving payment: {ex.Message}", ex);
            }
        }







        public async Task<bool> UpdatePayment(int id, UpdatePaymentDTO updatePaymentDTO, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid payment ID");
            }

            if (updatePaymentDTO == null)
            {
                throw new ArgumentNullException(nameof(updatePaymentDTO), "Payment data is required");
            }

            try
            {
                var payment = await _paymentRepository.GetPaymentById(id, cancellationToken);
                if (payment == null)
                {
                    _logger.LogWarning("Payment with ID {id} not found", id);
                    throw new KeyNotFoundException("Payment not found");
                }


                payment.PaymentDate = updatePaymentDTO.PaymentDate ?? payment.PaymentDate;
                payment.PaymentStatus = updatePaymentDTO.PaymentStatus ?? payment.PaymentStatus;

                int rowsAffected = await _paymentRepository.UpdatePayment(payment, cancellationToken);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating payment with ID {id}", id);
                throw new ApplicationException("Error while updating payment", ex);
            }
        }




        public async Task<bool> DeletePayment(int paymentId, CancellationToken cancellationToken)
        {
            if (paymentId <= 0)
            {
                throw new ArgumentException("Invalid payment ID");
            }

            try
            {
                return await _paymentRepository.DeletePayment(paymentId, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting payment with ID {paymentId}", paymentId);
                throw new ApplicationException("Error while deleting payment", ex);
            }
        }

        public async Task<List<PaymentResponseDTO>> GetAllPayments(CancellationToken cancellationToken)
        {
            var payments = await _paymentRepository.GetAllPayments(cancellationToken);
            return _mapper.Map<List<PaymentResponseDTO>>(payments);
        }

        public async Task<(List<PaymentResponseDTO> Data, int TotalCount)> GetPaymentPaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                _logger.LogWarning("Invalid pagination parameters: PageNumber={pageNumber}, PageSize={pageSize}", pageNumber, pageSize);
                throw new ArgumentException("Invalid pagination parameters");
            }

            var (payments, totalCount) = await _paymentRepository.GetPaymentPaged(pageNumber, pageSize, cancellationToken);
            return (_mapper.Map<List<PaymentResponseDTO>>(payments), totalCount);
        }

        public async Task<bool> HandlePaymentSuccess(int paymentId, CancellationToken cancellationToken)
        {
            if (paymentId <= 0)
            {
                throw new ArgumentException("Invalid payment ID");
            }

            try
            {
                var payment = await _paymentRepository.GetPaymentById(paymentId, cancellationToken);
                if (payment == null)
                {
                    _logger.LogWarning("Payment with ID {paymentId} not found", paymentId);
                    throw new KeyNotFoundException("Payment not found");
                }

                // Kiểm tra nếu PaymentStatus đã là Paid thì không cập nhật nữa
                if (payment.PaymentStatus == PaymentStatus.Paid)
                {
                    _logger.LogInformation("Payment with ID {paymentId} is already marked as Paid", paymentId);
                    return false;
                }

                // Cập nhật trạng thái thanh toán thành Paid
                payment.PaymentStatus = PaymentStatus.Paid;
                payment.PaymentDate = DateTime.UtcNow; // Cập nhật thời gian thanh toán hoàn thành

                int rowsAffected = await _paymentRepository.UpdatePayment(payment, cancellationToken);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling payment success for payment ID {paymentId}", paymentId);
                throw new ApplicationException("Error while processing payment success", ex);
            }
        }

        public async Task<bool> HandlePaymentFailure(int paymentId, CancellationToken cancellationToken)
        {
            if (paymentId <= 0)
            {
                throw new ArgumentException("Invalid payment ID");
            }

            try
            {
                var payment = await _paymentRepository.GetPaymentById(paymentId, cancellationToken);
                if (payment == null)
                {
                    _logger.LogWarning("Payment with ID {paymentId} not found", paymentId);
                    throw new KeyNotFoundException("Payment not found");
                }

                // Nếu PaymentStatus đã là Aborted, không cập nhật lại
                if (payment.PaymentStatus == PaymentStatus.Aborted)
                {
                    _logger.LogInformation("Payment with ID {paymentId} is already marked as Aborted", paymentId);
                    return false;
                }

                // Cập nhật trạng thái thành Aborted
                payment.PaymentStatus = PaymentStatus.Aborted;
                payment.PaymentDate = DateTime.UtcNow; // Ghi nhận thời gian thất bại

                int rowsAffected = await _paymentRepository.UpdatePayment(payment, cancellationToken);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling payment failure for payment ID {paymentId}", paymentId);
                throw new ApplicationException("Error while processing payment failure", ex);
            }
        }


    }
}
