using BusinessLayer.RequestModel.OrderDetail;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SWD392_SportShop.Controllers
{
    [Route("API/Order-Detail")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        //[HttpPost("Search")]
        //public async Task<IActionResult> GetAllOrderDetails(GetAllOrderDetailRequestModel model)
        //{
        //    try
        //    {
        //        var result = await _orderDetailService.GetAllOrderDetails(model);
        //        return StatusCode(result.Code, result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "An error occurred: " + ex.Message });

        //    }
        //}
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> AddOrderDetails(CreateOrderDetailRequestModel model)
        {
            try
            {
                var result = await _orderDetailService.AddOrderDetailAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailsById(int id)
        {
            try
            {
                var result = await _orderDetailService.GetOrderDetailById(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
            }
        }

    }
}
