using BusinessLayer.Service;
using BusinessLayer.Service.Implement;
using BusinessLayer.Service.PaymentService.VnPay.Request;
using DataLayer.DBContext;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayPalCheckoutSdk.Orders;
using System.Net;
using System.Security.Policy;

namespace SWD392_SportShop.Controllers
{
    [Route("API/Payment/")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private db_aad141_swd392Context _swd392Context;
        private readonly IOrderService _orderService;
        private readonly IVnPayService _vnPayService;
        private readonly IPaymentService _paymentService;
        private readonly PayPalService _payPalService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(db_aad141_swd392Context context,
            IOrderService orderService, IVnPayService vpnPayService, 
            IPaymentService paymentService, PayPalService payPalService,
            ILogger<PaymentController> logger
            )
        {
            _swd392Context = context;
            _vnPayService = vpnPayService;
            _orderService = orderService;
            _paymentService = paymentService;
            _payPalService = payPalService;
            _logger = logger;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(decimal amount)
        {
            var client = _payPalService.GetHttpClient();
            var orderRequest = new OrdersCreateRequest();
            orderRequest.Prefer("return=representation");
            orderRequest.RequestBody(new OrderRequest
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>
            {
                new PurchaseUnitRequest
                {
                    AmountWithBreakdown = new AmountWithBreakdown
                    {
                        CurrencyCode = "USD",
                        Value = amount.ToString("F2")
                    }
                }
            },
                ApplicationContext = new ApplicationContext
                {
                    ReturnUrl = "https://example.com/return",  // URL để quay lại sau khi thanh toán thành công
                    CancelUrl = "https://example.com/cancel"   // URL để quay lại nếu hủy thanh toán
                }
            });

            var response = await client.Execute(orderRequest);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                return StatusCode((int)response.StatusCode, response.Result<object>());
            }
            var result = response.Result<PayPalCheckoutSdk.Orders.Order>();

            // Lấy link thanh toán
            var approvalLink = result.Links.FirstOrDefault(link => link.Rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.Href;

            if (approvalLink == null)
            {
                return BadRequest("Could not create PayPal order.");
            }

            return Ok(new { ApprovalUrl = approvalLink });
        }

        [Authorize]
        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] VnPayPaymentRequestModel model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.OrderId) || model.Amount <= 0)
                {
                    return BadRequest(new { Message = "Invalid payment request." });
                }

                var order = await _orderService.GetOrderById(model.OrderId);
                if (order == null || order.Data == null)
                {
                    return NotFound(new { Message = "Order not found." });
                }

                // Tạo URL thanh toán VnPay
                var paymentUrl = _vnPayService.CreatePaymentUrl(model, HttpContext);
                if (string.IsNullOrEmpty(paymentUrl))
                {
                    return StatusCode(500, new { Message = "Failed to create payment URL." });
                }

                return Redirect(paymentUrl); // Redirect tới URL thanh toán
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating VnPay payment URL: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { Message = "Internal server error while creating payment URL." });
            }
        }



        [Authorize(Roles = "User ")]
        [HttpGet("ExecutePayment")]

        public IActionResult ExecutePayment()
        {
            try
            {
                var response = _vnPayService.PaymentExecute(Request.Query);
                if (!response.Success)
                {
                    return BadRequest(new { Message = "Payment failed or signature mismatch." });
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error executing VnPay payment: {ex.Message}");
                return StatusCode(500, new { Message = ex.Message });
            }
        }
        
    }
}
