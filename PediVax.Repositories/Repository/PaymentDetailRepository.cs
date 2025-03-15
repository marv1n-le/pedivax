using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository.BaseRepository;

namespace PediVax.Repositories.Repository
{
    public class PaymentDetailRepository : GenericRepository<PaymentDetail>, IPaymentDetailRepository
    {
        public PaymentDetailRepository() : base() 
        {}

        public async Task<List<PaymentDetail>> GetAllPaymentDetails(CancellationToken cancellationToken)
        {
            return await _context.PaymentDetails
                .Include(pd => pd.Payment)
                .Include(pd => pd.VaccinePackageDetail)
                .ThenInclude(vpd => vpd.Vaccine)
                .ToListAsync(cancellationToken);
        }

        public async Task<PaymentDetail> GetPaymentDetailById(int paymentDetailId, CancellationToken cancellationToken)
        {
            return await _context.PaymentDetails
                .Include(pd => pd.Payment)
                .Include(pd => pd.VaccinePackageDetail)
                .ThenInclude(vpd => vpd.Vaccine)
                .FirstOrDefaultAsync(pd => pd.PaymentDetailId == paymentDetailId, cancellationToken);
        }

        public async Task<IEnumerable<PaymentDetail>> GetPaymentDetailByPaymentId(int paymentId, CancellationToken cancellationToken)
        {
            return await _context.PaymentDetails
                .Include(pd => pd.VaccinePackageDetail)
                .ThenInclude(vpd => vpd.Vaccine)
                .Where(pd => pd.PaymentId == paymentId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<PaymentDetail>> GetUncompletedByPaymentId(int paymentId, CancellationToken cancellationToken)
        {
            return await _context.PaymentDetails
                .Include(pd => pd.VaccinePackageDetail)
                .ThenInclude(vpd => vpd.Vaccine)
                .Where(pd => pd.PaymentId == paymentId && pd.IsCompleted == EnumList.IsCompleted.No)
                .ToListAsync(cancellationToken);
        }

        public async Task GeneratePaymentDetail(List<PaymentDetail> paymentDetails, CancellationToken cancellationToken)
        {
            _context.PaymentDetails.AddRange(paymentDetails);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> UpdatePaymentDetail(PaymentDetail paymentDetail, CancellationToken cancellationToken)
        {
            return await UpdateAsync(paymentDetail, cancellationToken);
        }

    }
}
