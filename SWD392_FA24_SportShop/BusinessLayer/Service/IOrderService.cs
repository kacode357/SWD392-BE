using BusinessLayer.RequestModel.Order;
using BusinessLayer.ResponseModel.Order;
using BusinessLayer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IOrderService
    {
        Task<BaseResponse<OrderResponseModel>> CreateOrderAsync(CreateOrderRequestModel model);
        Task<BaseResponse<OrderResponseModel>> UpdateOrderAsync(CreateOrderRequestModel model, int id);
        Task<BaseResponse<OrderResponseModel>> DeleteOrderAsync(int orderId, int status);
        Task<BaseResponse<OrderResponseModel>> GetOrderById(int orderId);
        Task<DynamicResponse<OrderResponseModel>> GetAllOrders(GetAllOrderRequestModel model);
        //Task<BaseResponse<OrderResponseModel>> CalculatePrice (int orderId);
        //Task<BaseResponse<OrderResponseModel>> GetOrdersByUserIdAsync (int userId);
        //Task<BaseResponse<OrderResponseModel>> GetOrderDetailsByOrderIdAsync (int orderId);
        Task<BaseResponse<OrderResponseModel>> ChangeOrderStatusAsync (int orderId, string jwtToken, int newStatus);
        Task<BaseResponse<OrderResponseModel>> ProcessRefundAsync (int orderId);
    }
}
