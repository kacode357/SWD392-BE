using BusinessLayer.RequestModel.Payment;
using BusinessLayer.ResponseModel;
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
using System.Security.Cryptography;
using System.Security.Policy;

namespace BusinessLayer.Service.Implement
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IShirtSizeRepository _shirtSizeRepository;

        public VnPayService(IConfiguration configuration,IPaymentRepository paymentRepository, IOrderRepository orderRepository, IShirtSizeRepository shirtSizeRepository)
        {
            _configuration = configuration;
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _shirtSizeRepository = shirtSizeRepository;
        }

        public async Task<BaseResponse<UrlResponseModel>> CreatePaymentUrl(VnPayPaymentRequestModel model, HttpContext context)
        {
            try
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
                pay.AddRequestData("vnp_CreateDate", model.CreateDate.ToString("yyyyMMddHHmmss"));
                pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
                pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
                pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
                pay.AddRequestData("vnp_OrderInfo", model.OrderId);
                pay.AddRequestData("vnp_OrderType", "order");
                pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
                pay.AddRequestData("vnp_TxnRef", tick);

                var paymentUrl =
                    pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);
                return new BaseResponse<UrlResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Create Url payment VnPay with order = " + model.OrderId + " success!.",
                    Data = new UrlResponseModel()
                    {
                        Url = paymentUrl
                    }
                };        
            } 
            catch (Exception ex)
            {
                return new BaseResponse<UrlResponseModel>()
                {
                    Code = 500,
                    Success = true,
                    Message = "Server Error!.",
                    Data = null,
                };
            }
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

        public async Task<BaseResponse> AddPayment(VnPayCallBackModel model)
        {
            try
            {
                //string myHashSecure;
                //string rawData = _configuration["Vnpay:HashSecret"] 
                //    + "&" + "vnp_Amount=" + model.vnp_Amount.ToString()
                //    + "&" + "vnp_BankCode=" + model.vnp_BankCode.ToString()
                //    + "&" + "vnp_BankTranNo=" + model.vnp_BankTranNo.ToString()
                //    + "&" + "vnp_CardType=" + model.vnp_CardType.ToString()
                //    + "&" + "vnp_OrderInfo=" + model.vnp_OrderInfo.ToString()
                //    + "&" + "vnp_PayDate=" + model.vnp_PayDate.ToString()
                //    + "&" + "vnp_ResponseCode=" + model.vnp_ResponseCode.ToString()
                //    + "&" + "vnp_TmnCode=" + model.vnp_TmnCode.ToString()
                //    + "&" + "vnp_TransactionNo=" + model.vnp_TransactionNo.ToString()
                //    + "&" + "vnp_TransactionStatus=" + model.vnp_TransactionStatus.ToString()
                //    + "&" + "vnp_TxnRef=" + model.vnp_TxnRef.ToString() ;

                //using (SHA256 sha256 = SHA256.Create())
                //{
                //    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                //    StringBuilder hashBuilder = new StringBuilder();
                //    foreach (var b in bytes)
                //    {
                //        hashBuilder.Append(b.ToString("x2")); // Chuyển đổi thành chuỗi hexa
                //    }

                //    myHashSecure =  hashBuilder.ToString(); // Trả về mã hash dạng chuỗi hexa
                //}
                //if (!myHashSecure.Equals(model.vnp_SecureHash))
                //{
                //    return new BaseResponse()
                //    {
                //        Code = 401,
                //        Success = false,
                //        Message = "Data has been changed!."
                //    };
                //}
                if (model.vnp_ResponseCode.Equals("00"))
                {
                    var order = await _orderRepository.GetOrderByIdAsync(model.vnp_OrderInfo);
                    if (order == null)
                    {
                        return new BaseResponse()
                        {
                            Code = 404,
                            Success = false,
                            Message = "Order not found."
                        };
                    }

                    var payment = new Payment()
                    {
                        UserId = order.UserId,
                        OrderId = model.vnp_OrderInfo.ToString(),
                        Date = model.vnp_PayDate.ToString(),
                        Amount = double.Parse(model.vnp_Amount.ToString()) / 100,
                        Method = "VnPay",
                        Description = model.vnp_OrderInfo.ToString(),
                        Status = true,
                    };
                    await _paymentRepository.CreatePaymentAsync(payment);

                    order.Status = 2;
                    await _orderRepository.UpdateOrderAsync(order);
                    foreach (var orderDetail in order.OrderDetails)
                    {
                        var shirtSize = await _shirtSizeRepository.GetShirtSizeByIdAsync(orderDetail.ShirtSizeId);

                        if (shirtSize != null && shirtSize.Quantity >= orderDetail.Quantity)
                        {
                            shirtSize.Quantity -= orderDetail.Quantity;
                            await _shirtSizeRepository.UpdateShirtSizeAsync(shirtSize);
                        }
                        else
                        {
                            return new BaseResponse()
                            {
                                Code = 400,
                                Success = false,
                                Message = "Insufficient stock for item."
                            };
                        }
                    }
                    return new BaseResponse()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Giao dịch thành công!."
                    };
                }
                else
                {
                    string message;
                    switch (model.vnp_ResponseCode)
                    {
                        case "05":
                            message = "Giao dịch không thành công: Tài khoản không đủ tiền.";
                            break;
                        case "09":
                            message = "Giao dịch đã bị hủy.";
                            break;
                        case "13":
                            message = "Giao dịch thất bại: Xác thực không thành công.";
                            break;
                        case "24":
                            message = "Giao dịch đã bị khách hàng hủy.";
                            break;
                        case "51":
                            message = "Giao dịch không thành công: Tài khoản không đủ tiền.";
                            break;
                        case "65":
                            message = "Giao dịch không thành công: Giao dịch bị giới hạn tần suất.";
                            break;
                        case "91":
                            message = "Không kết nối được với ngân hàng phát hành thẻ.";
                            break;
                        case "99":
                            message = "Lỗi hệ thống, vui lòng thử lại sau.";
                            break;
                        default:
                            message = "Giao dịch không thành công: Mã lỗi không xác định.";
                            break;
                    }
                    return new BaseResponse()
                    {
                        Code = 400,
                        Success = false,
                        Message = message
                    };
                }
                
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server error!."
                };
            }
        }
    }
}
