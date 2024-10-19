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

        public PaymentController(IOrderService orderService, IVnPayService vpnPayService)
        {
            _vnPayService = vpnPayService;
            _orderService = orderService;
        }

        [Authorize(Roles = "User ")]
        [HttpPost("CreatePayment")]
        public IActionResult CreatePaymentUrl([FromBody] VnPayPaymentRequestModel model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.OrderId) || model.Amount <= 0)
                {
                    return BadRequest(new { Message = "Invalid payment request." });
                }

                var paymentUrl = _vnPayService.CreatePaymentUrl(model, HttpContext);
                return Redirect(paymentUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [Authorize(Roles = "User ")]
        [HttpPost("ExecutePayment")]
        public IActionResult ExecutePayment()
        {
            try
            {
                var response = _vnPayService.PaymentExecute(Request.Query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });

            }
        }
        
    }
}
