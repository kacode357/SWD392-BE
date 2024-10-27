using BusinessLayer.RequestModel.Payment;
using BusinessLayer.ResponseModels;
using BusinessLayer.Service;
using BusinessLayer.Service.Implement;
using BusinessLayer.Service.PaymentService.VnPay.Request;
using DataLayer.DBContext;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayPalCheckoutSdk.Orders;
using System.Net;
using System.Security.Claims;
using System.Security.Policy;

namespace SWD392_SportShop.Controllers
{
    [Route("API/Payment/")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        //private db_aad141_swd392Context _swd392Context;
        private readonly IOrderService _orderService;
        private readonly IVnPayService _vnPayService;
        private readonly IPaymentService _paymentService;
        //private readonly PayPalService _payPalService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(
            IOrderService orderService, IVnPayService vpnPayService, 
            IPaymentService paymentService, ILogger<PaymentController> logger
            )
        {
            //_swd392Context = context;
            _vnPayService = vpnPayService;
            _orderService = orderService;
            _paymentService = paymentService;
            //_payPalService = payPalService;
            _logger = logger;
        }

        //[HttpPost("create-order")]
        //public async Task<IActionResult> CreateOrder(decimal amount)
        //{
        //    var client = _payPalService.GetHttpClient();
        //    var orderRequest = new OrdersCreateRequest();
        //    orderRequest.Prefer("return=representation");
        //    orderRequest.RequestBody(new OrderRequest
        //    {
        //        CheckoutPaymentIntent = "CAPTURE",
        //        PurchaseUnits = new List<PurchaseUnitRequest>
        //    {
        //        new PurchaseUnitRequest
        //        {
        //            AmountWithBreakdown = new AmountWithBreakdown
        //            {
        //                CurrencyCode = "USD",
        //                Value = amount.ToString("F2")
        //            }
        //        }
        //    },
        //        ApplicationContext = new ApplicationContext
        //        {
        //            ReturnUrl = "https://example.com/return",  // URL để quay lại sau khi thanh toán thành công
        //            CancelUrl = "https://example.com/cancel"   // URL để quay lại nếu hủy thanh toán
        //        }
        //    });

        //    var response = await client.Execute(orderRequest);
        //    if (response.StatusCode != HttpStatusCode.Created)
        //    {
        //        return StatusCode((int)response.StatusCode, response.Result<object>());
        //    }
        //    var result = response.Result<PayPalCheckoutSdk.Orders.Order>();

        //    // Lấy link thanh toán
        //    var approvalLink = result.Links.FirstOrDefault(link => link.Rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.Href;

        //    if (approvalLink == null)
        //    {
        //        return BadRequest("Could not create PayPal order.");
        //    }

        //    return Ok(new { ApprovalUrl = approvalLink });
        //}

        [Authorize]
        [HttpPost("GetUrlPayment")]
        public async Task<IActionResult> GetUrlPayment([FromBody] VnPayPaymentRequestModel model )
        {
            try
            {
                var result = await _vnPayService.CreatePaymentUrl(model, HttpContext);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating VnPay payment URL: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { Message = "Internal server error while creating payment URL." });
            }
        }
        [HttpGet("CreatePayment")]
        public async Task<IActionResult> CreatePaymentUrl([FromQuery] VnPayCallBackModel model)
        {
            try
            {
                var result = await _vnPayService.AddPayment(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("Search")]
        public async Task<IActionResult> GetAllPayment(GetAllPaymentRequestModel model)
        {
            try
            {
                var result = await _paymentService.GetPaymentsAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("PaymentByCurrentUser")]
        public async Task<IActionResult> GetPaymentByUserId(GetAllPaymentRequestModel model)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return StatusCode(401, new BaseResponse()
                    {
                        Code = 401,
                        Success = false,
                        Message = "User information not found, user may not be authenticated into the system!."
                    });
                }
                var result = await _paymentService.GetPaymentByUserId(model , int.Parse(userId));
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        //[Authorize(Roles = "User ")]
        //[HttpGet("ExecutePayment")]

        //public IActionResult ExecutePayment()
        //{
        //    try
        //    {
        //        var response = _vnPayService.PaymentExecute(Request.Query);
        //        if (!response.Success)
        //        {
        //            return BadRequest(new { Message = "Payment failed or signature mismatch." });
        //        }
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error executing VnPay payment: {ex.Message}");
        //        return StatusCode(500, new { Message = ex.Message });
        //    }
        //}



    }
}
