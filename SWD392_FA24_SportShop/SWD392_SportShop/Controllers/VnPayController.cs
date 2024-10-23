//using BusinessLayer.Service;
//using BusinessLayer.Service.PaymentService.VnPay.lib;
//using BusinessLayer.Service.PaymentService.VnPay.Request;
//using DataLayer.DBContext;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace SWD392_SportShop.Controllers
//{
//    [Route("API/VnPay")]
//    [ApiController]
//    public class VnPayController : ControllerBase
//    {
//        protected readonly db_aad141_swd392Context _context;
//        private readonly IConfiguration _configuration;
//        private readonly IOrderService _orderService;
//        private readonly IPaymentService _paymentService;

//        public VnPayController(IConfiguration configuration, db_aad141_swd392Context context,
//            IOrderService orderService, IPaymentService paymentService)
//        {
//            _configuration = configuration;
//            _context = context;
//            _orderService = orderService;
//            _paymentService = paymentService;
//        }

//        [HttpGet]
//        [AllowAnonymous]
//        public async IActionResult Get(VnPayPaymentRequestModel model, HttpContext context, int userId)
//        {
//            try
//            {
//                var check = await _orderService.GetCartByCurrentUser(userId);
//                if (check != null)
//                {
//                    var order = await _orderService.AddToCart();

//                    var tick = DateTime.Now.Ticks.ToString();
//                    var pay = new VnPayLibrary();
//                    var urlCallBack = _configuration["Vnpay:ReturnUrl"];

//                    pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
//                    pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
//                    pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
//                    pay.AddRequestData("vnp_Amount", (model.Amount * 100).ToString());
//                    pay.AddRequestData("vnp_CreateDate", model.CreatedDate.ToString("yyyyMMddHHmmss"));
//                    pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
//                    pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
//                    pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
//                    pay.AddRequestData("vnp_OrderInfo", $"{model.Name} {model.OrderDescription} {model.Amount} {model.OrderId}");
//                    pay.AddRequestData("vnp_OrderType", "order");
//                    pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
//                    pay.AddRequestData("vnp_TxnRef", tick);

//                    var payment = await _paymentService.CreatePaymentAsync(order.Id, pay);
//                }

//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }


//        }
//    }
