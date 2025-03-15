using PediVax.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Repositories.IRepository
{
    public interface IPaymentDetailRepository
    {
        Task<List<PaymentDetail>> GetAllPaymentDetails(CancellationToken cancellationToken);
        Task<PaymentDetail> GetPaymentDetailById(int id, CancellationToken cancellationToken);
        Task<IEnumerable<PaymentDetail>> GetPaymentDetailByPaymentId(int paymentId, CancellationToken cancellationToken);
        Task<IEnumerable<PaymentDetail>> GetUncompletedByPaymentId(int paymentId, CancellationToken cancellationToken);
        Task GeneratePaymentDetail(List<PaymentDetail> paymentDetails, CancellationToken cancellationToken);
        Task<int> UpdatePaymentDetail(PaymentDetail paymentDetail, CancellationToken cancellationToken);
    }
}
