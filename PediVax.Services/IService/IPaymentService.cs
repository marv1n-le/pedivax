using PediVax.BusinessObjects.DTO.PaymentDTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Services.IService
{
    public interface IPaymentService
    {
        Task<List<PaymentResponseDTO>> GetAllPayments(CancellationToken cancellationToken);
        Task<PaymentResponseDTO> GetPaymentById(int paymentId, CancellationToken cancellationToken);
        Task<(List<PaymentResponseDTO> Data, int TotalCount)> GetPaymentPaged(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<PaymentResponseDTO> AddPayment(CreatePaymentDTO createPaymentDTO, CancellationToken cancellationToken);
        Task<bool> UpdatePayment(int id, UpdatePaymentDTO updatePaymentDTO, CancellationToken cancellationToken);
        Task<bool> DeletePayment(int id, CancellationToken cancellationToken);
        Task<bool> HandlePaymentSuccess(int paymentId, CancellationToken cancellationToken);
        Task<bool> HandlePaymentFailure(int paymentId, CancellationToken cancellationToken);



    }
}
