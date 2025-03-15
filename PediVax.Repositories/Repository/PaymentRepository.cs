﻿using Microsoft.EntityFrameworkCore;
using PediVax.BusinessObjects.DBContext;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository.BaseRepository;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Repositories.Repository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {

        public PaymentRepository() : base()
        {
        }

        // Sửa lại GetAllPayments để sử dụng trực tiếp DbContext
        public async Task<List<Payment>> GetAllPayments(CancellationToken cancellationToken)
        {
            return await _context.Payments.ToListAsync(cancellationToken);
        }

        // Sửa lại GetPaymentById để sử dụng trực tiếp DbContext
        public async Task<Payment?> GetPaymentById(int paymentId, CancellationToken cancellationToken)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId, cancellationToken);
        }

        // Các phương thức khác vẫn giữ nguyên
        public async Task<(List<Payment>, int)> GetPaymentPaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await GetPagedAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task<int> CreatePayment(Payment payment, CancellationToken cancellationToken)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment.PaymentId;
        }

        public async Task<int> UpdatePayment(Payment payment, CancellationToken cancellationToken)
        {
            return await UpdateAsync(payment, cancellationToken);
        }

        public async Task<bool> DeletePayment(int paymentId, CancellationToken cancellationToken)
        {
            return await DeleteAsync(paymentId, cancellationToken);
        }
    }
}
