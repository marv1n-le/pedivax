using PediVax.BusinessObjects.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Repositories.IRepository
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllPayments(CancellationToken cancellationToken);
        Task<Payment?> GetPaymentById(int paymentId, CancellationToken cancellationToken);
        Task<(List<Payment>, int)> GetPaymentPaged(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<int> CreatePayment(Payment payment, CancellationToken cancellationToken);
        Task<int> UpdatePayment(Payment payment, CancellationToken cancellationToken);
        Task<bool> DeletePayment(int paymentId, CancellationToken cancellationToken);
    }
}
