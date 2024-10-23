using BusinessLayer.Service;
using BusinessLayer.Service.PaymentService.VnPay.Request;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace SWD392_SportShop.Controllers
{
    [Route("API/Payment/")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IVnPayService _vnPayService;
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IOrderService orderService, IVnPayService vpnPayService, IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _vnPayService = vpnPayService;
            _orderService = orderService;
            _paymentService = paymentService;
            _logger = logger;
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
                if (order == null)
                {
                    return NotFound(new { Message = "Order not found." });
                }

                var paymentUrl = _vnPayService.CreatePaymentUrl(model, HttpContext);
                return Redirect(paymentUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating VnPay payment URL: {ex.Message}");
                return StatusCode(500, new { Message = ex.Message });
            }
        }

<<<<<<< Updated upstream
        [Authorize]
        [HttpPost("ExecutePayment")]
=======
        [Authorize(Roles = "User ")]
        [HttpGet("ExecutePayment")]
>>>>>>> Stashed changes
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
