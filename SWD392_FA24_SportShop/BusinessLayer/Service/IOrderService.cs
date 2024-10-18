using BusinessLayer.RequestModel.Order;
using BusinessLayer.RequestModel.OrderDetail;
using BusinessLayer.ResponseModel.Order;
using BusinessLayer.ResponseModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IOrderService
    {
        //Task<BaseResponse<OrderResponseModel>> CreateOrderAsync(CreateOrderRequestModel model);
        Task<BaseResponse<OrderResponseModel>> UpdateOrderAsync(CreateOrderRequestModel model, string id);
        Task<BaseResponse<OrderResponseModel>> DeleteOrderAsync(string orderId, int status);
        Task<BaseResponse<OrderResponseModel>> GetOrderById(string orderId);
        Task<DynamicResponse<OrderResponseModel>> GetAllOrders(GetAllOrderRequestModel model);
        //Task<BaseResponse<OrderResponseModel>> CalculatePrice (int orderId);
        //Task<BaseResponse<OrderResponseModel>> GetOrdersByUserIdAsync (int userId);
        //Task<BaseResponse<OrderResponseModel>> GetOrderDetailsByOrderIdAsync (int orderId);
        Task<BaseResponse<OrderResponseModel>> ChangeOrderStatusAsync (string orderId, string jwtToken, int newStatus);
        Task<BaseResponse<OrderResponseModel>> ProcessRefundAsync (string orderId);
        Task<BaseResponse<CartResponseModel>> AddToCart(CreateOrderDetailsForCartRequestModel model, string? userId);
        Task<BaseResponse<CartResponseModel>> GetCartByCurrentUser(string? userId);
        Task<BaseResponse<CartResponseModel>> UpdateCart(UpdateCartRequestModel model);
    }
}
