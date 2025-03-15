using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.PaymentDetailDTO;
using PediVax.Services.IService;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace PediVax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly IPaymentDetailService _paymentDetailService;
        private readonly ILogger<PaymentDetailController> _logger;

        public PaymentDetailController(IPaymentDetailService paymentDetailService, ILogger<PaymentDetailController> logger)
        {
            _paymentDetailService = paymentDetailService;
            _logger = logger;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(List<PaymentDetailResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllPaymentDetails(CancellationToken cancellationToken)
        {
            var response = await _paymentDetailService.GetAllPaymentDetails(cancellationToken);
            if (response == null || response.Count == 0)
            {
                _logger.LogWarning("No payment details found.");
                return NotFound(new { message = "No payment details available." });
            }

            return Ok(response);
        }

        // Get Payment Detail by ID
        [HttpGet("get-by-id/{paymentDetailId}")]
        [ProducesResponseType(typeof(PaymentDetailResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPaymentDetailById(int paymentDetailId, CancellationToken cancellationToken)
        {
            if (paymentDetailId <= 0)
            {
                return BadRequest(new { message = "Invalid payment detail ID." });
            }

            try
            {
                var response = await _paymentDetailService.GetPaymentDetailById(paymentDetailId, cancellationToken);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Payment detail not found." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching payment detail.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while fetching payment detail." });
            }
        }

        [HttpGet("get-by-payment-id/{paymentId}")]
        [ProducesResponseType(typeof(List<PaymentDetailResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPaymentDetailByPaymentId(int paymentId, CancellationToken cancellationToken)
        {
            if (paymentId <= 0)
            {
                return BadRequest(new { message = "Invalid payment ID." });
            }

            try
            {
                var response = await _paymentDetailService.GetPaymentDetailByPaymentId(paymentId, cancellationToken);
                if (response == null || response.Count == 0)
                {
                    _logger.LogWarning("No payment details found for payment ID: {paymentId}", paymentId);
                    return NotFound(new { message = "No payment details found for the given payment ID." });
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching payment details for payment ID: {paymentId}", paymentId);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while fetching payment details." });
            }
        }

        // Get Uncompleted Payment Details by Payment ID
        [HttpGet("get-uncompleted-by-payment-id/{paymentId}")]
        [ProducesResponseType(typeof(List<PaymentDetailResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUncompletedByPaymentId(int paymentId, CancellationToken cancellationToken)
        {
            if (paymentId <= 0)
            {
                return BadRequest(new { message = "Invalid payment ID." });
            }

            try
            {
                var response = await _paymentDetailService.GetUncompletedByPaymentId(paymentId, cancellationToken);
                if (response == null || response.Count == 0)
                {
                    _logger.LogWarning("No uncompleted payment details found for payment ID: {paymentId}", paymentId);
                    return NotFound(new { message = "No uncompleted payment details found for the given payment ID." });
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching uncompleted payment details for payment ID: {paymentId}", paymentId);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while fetching uncompleted payment details." });
            }
        }

        // Generate Payment Details
        [HttpPost("generate/{paymentId}")]
        [ProducesResponseType(typeof(List<PaymentDetailResponseDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GeneratePaymentDetails(int paymentId, CancellationToken cancellationToken)
        {
            if (paymentId <= 0)
            {
                return BadRequest(new { message = "Invalid payment ID." });
            }

            try
            {
                var response = await _paymentDetailService.GeneratePaymentDetails(paymentId, cancellationToken);
                return CreatedAtAction(nameof(GeneratePaymentDetails), new { paymentId = paymentId }, response);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error generating payment details.");
                return Problem("An error occurred while generating payment details.");
            }
        }

        // Update Payment Detail
        [HttpPut("update/{paymentDetailId}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdatePaymentDetail(int paymentDetailId, [FromBody] UpdatePaymentDetailDTO request, CancellationToken cancellationToken)
        {
            if (paymentDetailId <= 0)
            {
                return BadRequest(new { message = "Invalid payment detail ID." });
            }

            try
            {
                var response = await _paymentDetailService.UpdatePaymentDetail(paymentDetailId, request, cancellationToken);
                return Ok(new { success = response });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Payment detail not found." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error updating payment detail with ID {paymentDetailId}", paymentDetailId);
                return Problem("An error occurred while updating payment detail.");
            }
        }

    }
}
