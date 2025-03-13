using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.VaccinePackageDetailDTO;
using PediVax.Services.IService;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaccinePackageDetailController : ControllerBase
    {
        private readonly IVaccinePackageDetailService _vaccinePackageDetailService;
        private readonly ILogger<VaccinePackageDetailController> _logger;

        public VaccinePackageDetailController(IVaccinePackageDetailService vaccinePackageDetailService, ILogger<VaccinePackageDetailController> logger)
        {
            _vaccinePackageDetailService = vaccinePackageDetailService;
            _logger = logger;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(List<VaccinePackageDetailResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllVaccinePackageDetails(CancellationToken cancellationToken)
        {
            var response = await _vaccinePackageDetailService.GetAllVaccinePackageDetails(cancellationToken);
            return Ok(response);
        }

        [HttpGet("get-by-id/{id}")]
        [ProducesResponseType(typeof(VaccinePackageDetailResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetVaccinePackageDetailById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _vaccinePackageDetailService.GetVaccinePackageDetailById(id, cancellationToken);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Vaccine Package Detail not found." });
            }
        }

        [HttpGet("get-by-vaccine-package-id/{vaccinePackageId}")]
        [ProducesResponseType(typeof(List<VaccinePackageDetailResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetVaccinePackageDetailByVaccinePackageId(int vaccinePackageId, CancellationToken cancellationToken)
        {
            var response = await _vaccinePackageDetailService.GetVaccinePackageDetailByVaccinePackageId(vaccinePackageId, cancellationToken);
            return Ok(response);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(VaccinePackageDetailResponseDTO), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddVaccinePackageDetail([FromBody] CreateVaccinePackageDetailDTO createDTO, CancellationToken cancellationToken)
        {
            var response = await _vaccinePackageDetailService.AddVaccinePackageDetail(createDTO, cancellationToken);
            return CreatedAtAction(nameof(GetVaccinePackageDetailById), new { id = response.VaccinePackageDetailId }, response);
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateVaccinePackageDetail(int id, [FromBody] UpdateVaccinePackageDetailDTO updateDTO, CancellationToken cancellationToken)
        {
            var response = await _vaccinePackageDetailService.UpdateVaccinePackageDetail(id, updateDTO, cancellationToken);
            return Ok(new { success = response });
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteVaccinePackageDetail(int id, CancellationToken cancellationToken)
        {
            var response = await _vaccinePackageDetailService.DeleteVaccinePackageDetail(id, cancellationToken);
            return Ok(new { success = response });
        }

        [HttpGet("get-paged/{pageNumber}/{pageSize}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetVaccinePackageDetailPaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var (data, totalCount) = await _vaccinePackageDetailService.GetVaccinePackageDetailPaged(pageNumber, pageSize, cancellationToken);
            return Ok(new { Data = data, TotalCount = totalCount });
        }
    }
}
