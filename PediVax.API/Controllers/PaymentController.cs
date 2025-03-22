using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.PaymentDTO;
using PediVax.BusinessObjects.DTO.VnPayDTO;
using PediVax.Services.IService;
using PediVax.Repositories.IRepository;
using System;
using System.Threading;
using System.Threading.Tasks;
using PediVax.BusinessObjects.DTO.DiseaseDTO;
using PediVax.Services.Service;
using System.Net;

namespace PediVax.API.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<VNPayController> _logger; // Thêm Logger

        public VNPayController(
            IVnPayService vnPayService,
            IPaymentService paymentService,
            IMapper mapper,
            IPaymentRepository paymentRepository,
            ILogger<VNPayController> logger) // Nhận logger vào constructor
        {
            _vnPayService = vnPayService;
            _paymentService = paymentService;
            _mapper = mapper;
            _paymentRepository = paymentRepository;
            _logger = logger; // Khởi tạo logger
        }


        [HttpGet("get-all")]
        [ProducesResponseType(typeof(List<PaymentResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllPayment(CancellationToken cancellationToken)
        {
            var response = await _paymentService.GetAllPayments(cancellationToken);
            if (response == null || response.Count == 0)
            {
                _logger.LogWarning("No Payment found.");
                return NotFound(new { message = "No Payment available." });
            }
            return Ok(response);
        }

        /// <summary>
        /// Tạo URL thanh toán VNPay
        /// </summary>
        [HttpPost("create-payment-url")]
        public IActionResult CreatePaymentUrl([FromBody] VnPaymentRequestModel model)
        {
            if (model == null || model.Amount <= 0)
            {
                return BadRequest(new { message = "Dữ liệu thanh toán không hợp lệ." });
            }

            var paymentUrl = _vnPayService.CreatePaymentUrl(HttpContext, model);
            return Ok(new { url = paymentUrl });
        }

        /// <summary>
        /// Xử lý phản hồi từ VNPay
        /// </summary>
        [HttpGet("payment-callback")]
        public IActionResult PaymentExecute()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if (response == null)
            {
                return BadRequest(new { message = "Lỗi xử lý thanh toán." });
            }
            return Ok(response);
        }
        [HttpPut("update-payment/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] UpdatePaymentDTO updatePaymentDTO, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "ID thanh toán không hợp lệ." });
            }

            if (updatePaymentDTO == null)
            {
                return BadRequest(new { message = "Dữ liệu thanh toán không hợp lệ." });
            }

            try
            {
                var updated = await _paymentService.UpdatePayment(id, updatePaymentDTO, cancellationToken);

                if (!updated)
                {
                    return NotFound(new { message = "Không tìm thấy thanh toán để cập nhật." });
                }

                return Ok(new { success = true, message = "Cập nhật thanh toán thành công." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật thanh toán {id}: {message}", id, ex.Message);
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi cập nhật thanh toán.", error = ex.Message });
            }
        }

        /// <summary>
        /// Thêm thanh toán mới
        /// </summary>
        [HttpPost("add-payment")]
        
        public async Task<IActionResult> AddPayment([FromBody] CreatePaymentDTO createPaymentDTO, CancellationToken cancellationToken)
        {
            if (createPaymentDTO == null)
            {
                return BadRequest(new { message = "Dữ liệu thanh toán không hợp lệ." });
            }

            try
            {
                var result = await _paymentService.AddPayment(createPaymentDTO, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi thêm thanh toán: {Message}", ex.Message);
                return StatusCode(500, new { message = "Lỗi khi thêm thanh toán.", error = ex.Message });
            }
        }


        /// <summary>
        /// Callback xử lý thanh toán
        /// </summary>
        [HttpGet("callbackUrl")]
        public async Task<IActionResult> PaymentCallback(CancellationToken cancellationToken)
        {
            var responseCode = Request.Query["vnp_ResponseCode"];
            var paymentIdStr = Request.Query["vnp_OrderInfo"];

            if (!int.TryParse(paymentIdStr, out int paymentId))
            {
                _logger.LogWarning("Invalid payment ID received in callback: {paymentIdStr}", paymentIdStr);
                return BadRequest("Invalid payment ID");
            }

            if (responseCode == "00")
            {
                // Thanh toán thành công
                var success = await _paymentService.HandlePaymentSuccess(paymentId, cancellationToken);
                if (!success)
                {
                    _logger.LogWarning("Payment {paymentId} was already marked as Paid", paymentId);
                }
                return Redirect("http://localhost:3000/success");
            }
            else
            {
                // Thanh toán thất bại
                var failed = await _paymentService.HandlePaymentFailure(paymentId, cancellationToken);
                if (!failed)
                {
                    _logger.LogWarning("Payment {paymentId} could not be updated to Aborted", paymentId);
                }
                return Redirect("http://localhost:3000/failed");
            }


        }
    }
}
