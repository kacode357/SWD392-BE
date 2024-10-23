using BusinessLayer.RequestModel.Payment;
using BusinessLayer.ResponseModel.Payment;
using BusinessLayer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IPaymentService
    {
        Task<BaseResponse<PaymentResponseModel>> CreatePaymentAsync(CreatePaymentRequestModel model);
        Task<DynamicResponse<PaymentResponseModel>> GetPaymentsAsync(GetAllPaymentRequestModel model);
        Task<BaseResponse<PaymentResponseModel>> UpdatePaymentAsync(CreatePaymentRequestModel model, int id);
        //Task<BaseResponse<PaymentResponseModel>> DeletePaymentAsync(int paymentId, int status);
        Task<BaseResponse<PaymentResponseModel>> GetPaymentById(int paymentId);
    }
}
