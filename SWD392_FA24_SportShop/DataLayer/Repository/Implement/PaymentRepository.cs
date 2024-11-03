using DataLayer.DBContext;
using DataLayer.DTO;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Implement
{
    public class PaymentRepository: IPaymentRepository
    {
        private readonly db_aad141_swd392Context _swd392Context;

        public PaymentRepository(db_aad141_swd392Context swd392Context)
        {
            _swd392Context = swd392Context;
        }

        public async Task<bool> CreatePaymentAsync(Payment payment)
        {
            try
            {
                _swd392Context.Payments.AddAsync(payment);
                await _swd392Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*public async Task<bool> DeletePaymentAsync(int paymentId)
        {
            try
            {
                var payment = await _swd392Context.Payments.FindAsync(paymentId);
                if (payment == null)
                {
                    return false;
                }
                _swd392Context.Payments.Remove(payment);
                return await _swd392Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        public async Task<List<Payment>> GetAllPayments()
        {
            try
            {
                return await _swd392Context.Payments
                    .Include(p => p.User)
                    .OrderByDescending(p => p.Id)
                    .ToListAsync(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Payment> GetPaymentById(int paymentId)
        {
            try
            {
                return await _swd392Context.Payments
                    .Include(p => p.User)
                    .Where(p => p.Id == paymentId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> HasSuccessFullPayment(int userId, int shirtSizeId)
        {
            try
            {
                return await _swd392Context.Payments
                    .Include(p => p.Order)
                    .AnyAsync(p => p.Order.UserId == userId && p.Order.OrderDetails.Any(od => od.ShirtSizeId == shirtSizeId) && 
                    p.Status == true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdatePaymentAsync(Payment paymentId)
        {
            try
            {
                _swd392Context.Payments.Update(paymentId);
                await _swd392Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Not found!" + ex);
            }
        }
    }
}
