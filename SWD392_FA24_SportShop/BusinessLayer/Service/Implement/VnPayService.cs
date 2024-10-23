using BusinessLayer.RequestModel.Payment;
using BusinessLayer.ResponseModel.Payment;
using BusinessLayer.ResponseModels;
using BusinessLayer.Service.PaymentService.VnPay.lib;
using BusinessLayer.Service.PaymentService.VnPay.Request;
using BusinessLayer.Service.PaymentService.VnPay.Response;
using DataLayer.Entities;
using DataLayer.Repository;
using DataLayer.Repository.Implement;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Implement
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;

        public VnPayService(IConfiguration configuration,IPaymentRepository paymentRepository, IOrderRepository orderRepository)
        {
            _configuration = configuration;
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
        }

        public string CreatePaymentUrl( VnPayPaymentRequestModel model, HttpContext context)
        {
            //var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            //var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
            var urlCallBack = _configuration["Vnpay:ReturnUrl"];
            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", (model.Amount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", model.OrderId);
            pay.AddRequestData("vnp_OrderType", "order");
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;
        }

        public VnPayPaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            var vnpay = new VnPayLibrary();
            foreach (var (key, value) in collections)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }

            var vnp_orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            var vnp_TransactionId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _configuration["VnPay:HashSecret"]);
            if (!checkSignature)
            {
                return new VnPayPaymentResponseModel
                {
                    Success = false
                };
            }

            return new VnPayPaymentResponseModel
            {
                Success = true,
                PaymentMethod = "VnPay",
                OrderDescription = vnp_OrderInfo,
                OrderId = vnp_orderId.ToString(),
                TransactionId = vnp_TransactionId.ToString(),
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode
            };
        }

        public async Task<BaseResponse<PaymentResponseModel>> AddPayment(VnPayCallBackModel model)
        {
            try
            {
                bool status = false;
                if (model.vnp_ResponseCode.ToString().Equals("00")){
                    status = true;
                }
                var order = await _orderRepository.GetOrderByIdAsync(model.vnp_OrderInfo);
                    var payment = new Payment()
                {
                    UserId = order.UserId,
                    OrderId = model.vnp_OrderInfo.ToString(),
                    Date = model.vnp_PayDate.ToString(),
                    Amount = double.Parse(model.vnp_Amount.ToString()) / 100,
                    Method = "VnPay",
                    Description = model.vnp_OrderInfo.ToString(),
                    Status = status,
                };
                await _paymentRepository.CreatePaymentAsync(payment);

                return new BaseResponse<PaymentResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Add Payment success!.",
                    Data = null,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PaymentResponseModel>()
                {
                    Code = 400,
                    Success = false,
                    Message = "Server error!.",
                    Data = null,
                };
            }
        }
    }
}
