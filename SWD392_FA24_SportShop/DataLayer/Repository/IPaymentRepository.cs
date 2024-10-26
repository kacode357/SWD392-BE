using DataLayer.DTO;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IPaymentRepository
    {
        Task<bool> CreatePaymentAsync (Payment payment);
        Task<bool> UpdatePaymentAsync (Payment paymentId);
        //Task<bool> DeletePaymentAsync (int  paymentId);
        Task<Payment> GetPaymentById (int paymentId);
        Task<List<Payment>> GetAllPayments();
        Task<bool> HasSuccessFullPayment(int userId, int shirtSizeId);
    }
}
