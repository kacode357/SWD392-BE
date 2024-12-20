﻿using BusinessLayer.RequestModel.Order;
using BusinessLayer.RequestModel.OrderDetail;
using BusinessLayer.Service;
using BusinessLayer.Service.PaymentService.VnPay.Request;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SWD392_SportShop.Controllers
{
    [Route("API/Order/")]
    [ApiController]
    public class OrderController: ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "Admin,Manager,Staff")]
        [HttpPost("Search")]
        public async Task<IActionResult> GetAllOrders(SearchOrderByIdRequestModel model)
        {
            try
            {
                if (model.Status == 0)
                {
                    model.Status = null;
                }

                var result = await _orderService.GetAllOrders(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
            }
        }

        [Authorize(Roles = "Admin,Manager,Staff")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            try
            {
                var result = await _orderService.GetOrderById(id);
                return StatusCode(result.Code, result);
            }
             catch(Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
            }
        }

        [Authorize]
        [HttpPost("user/Search")]
        public async Task<IActionResult> GetOrdersByCurrentUser([FromBody] GetOrderByCurrentUserRequestModel model)
        {
            try
            {
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!int.TryParse(userIdString, out int userId))
                {
                    return BadRequest(new { Message = "Invalid or missing user ID in token." });
                }
                
                var result = await _orderService.GetOrdersByCurrentUser(model, userId);

                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"An error occurred while processing your request: {ex.Message}" });
            }
        }


        //[HttpGet("Details/{orderId}")]
        //public async Task<IActionResult> GetOrderDetailsByOrderIdAsync(int id)
        //{
        //    try
        //    {
        //        var result = await _orderService.GetOrderDetailsByOrderIdAsync(id);
        //        return StatusCode(result.Code, result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
        //    }
        //}
        //[Authorize]
        //[HttpGet("userOrders")]
        //public async Task<IActionResult> GetOrdersByUserIdAsync()
        //{
        //    try
        //    {
        //        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //        if (int.TryParse(userIdString, out var userId))
        //        {
        //            var result = await _orderService.GetOrdersByUserIdAsync(userId);
        //            return StatusCode(result.Code, result);
        //        }
        //        else
        //        {
        //            return BadRequest(new { Message = "Invalid user ID." });
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
        //    }
        //}
        /*
         [Authorize(Roles = "User")]
         [HttpPost]
         public async Task<IActionResult> CreateOrder(CreateOrderRequestModel model)
         {
             try
             {
                 var result = await _orderService.CreateOrderAsync(model);
                 return StatusCode(result.Code, result);
             }
             catch (Exception ex)
             {
                 return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
             }
         }
        */
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody]CreateOrderRequestModel model, string id)
        {
            try
            {
                var result = await _orderService.UpdateOrderAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
            }
        }
        /*[Authorize(Roles = "User")]
        [HttpPost("Change-Status/{id}")]
        public async Task<IActionResult> DeleteOrder(int id, int status)
        {
            try
            {
                var result = await _orderService.DeleteOrderAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }*/
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(string id, int status)
        {
            try
            {
                var result = await _orderService.DeleteOrderAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
            }
        }
        [Authorize]
        [HttpPut("Change-Status/{id}")]
        public async Task<IActionResult> ChangeOrderStatus (string id, [FromBody] ChangeOrderStatusRequestModel model)
        {
            try
            {
                if (model == null || model.NewStatus < 0)
                {
                    return BadRequest(new { Message = "Invalid status value." });
                }

                var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(jwtToken))
                {
                    return Unauthorized(new { Message = "Token is required." });
                }

                var result = await _orderService.ChangeOrderStatusAsync(id, jwtToken, model.NewStatus);

                return StatusCode(result.Code, result);
            }
            catch (KeyNotFoundException ex)
            {
                // Nếu không tìm thấy đơn hàng hoặc thông tin gì đó
                return NotFound(new { Message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                // Nếu có vấn đề về quyền truy cập
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                // Lỗi không xác định
                return StatusCode(500, new { Message = "An unexpected error occurred: " + ex.Message });
            }
        }
        [Authorize(Roles = "Admin,Manager,Staff")]
        [HttpPost("Process-Refund/{id}")]
        public async Task<IActionResult> ProcessRefund(string id)
        {
            try
            {
                var result = await _orderService.ProcessRefundAsync(id);

                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
            }
        }
        [Authorize]
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(CreateOrderDetailsForCartRequestModel model)
        {
            try
            {
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdString, out int userId)) // Convert userId to int
                {
                    var result = await _orderService.AddToCart(model, userId);
                    return StatusCode(result.Code, result);
                }
                else
                {
                    return BadRequest(new { Message = "Invalid user ID." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
            }
        }

        [Authorize]
        [HttpPost("UpdateCart")]
        public async Task<IActionResult> UpdateCart(UpdateCartRequestModel model)
        {
            try
            {
                var result = await _orderService.UpdateCart(model);

                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
            }
        }
        [Authorize]
        [HttpPost("DeteleItemInCart")]
        public async Task<IActionResult> DeteleItemInCart(DeteleItemInCartRequestModel model)
        {
            try
            {
                var result = await _orderService.DeteteItemInCart(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
            }
        }
        [Authorize]
        [HttpGet("Cart")]
        public async Task<IActionResult> GetCardByCurrentUser()
        {
            try
            {
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdString, out int userId)) // Convert userId to int
                {
                    var result = await _orderService.GetCartByCurrentUser(userId);
                    return StatusCode(result.Code, result);
                }
                else
                {
                    return BadRequest(new { Message = "Invalid user ID." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
            }
        }

    }
}
