using PediVax.BusinessObjects.DTO.PaymentDetailDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Services.IService
{
    public interface IPaymentDetailService
    {
        Task<List<PaymentDetailResponseDTO>> GetAllPaymentDetails(CancellationToken cancellationToken);
        Task<PaymentDetailResponseDTO> GetPaymentDetailById(int id, CancellationToken cancellationToken);
        Task<List<PaymentDetailResponseDTO>> GeneratePaymentDetails(int paymentId, CancellationToken cancellationToken);
        Task<List<PaymentDetailResponseDTO>> GetPaymentDetailByPaymentId(int paymentId,
            CancellationToken cancellationToken);
        Task<List<PaymentDetailResponseDTO>> GetUncompletedByPaymentId(int paymentId,
            CancellationToken cancellationToken);
        Task<bool> UpdatePaymentDetail(int id, UpdatePaymentDetailDTO updatePaymentDetailDTO,
            CancellationToken cancellationToken);
    }
}
