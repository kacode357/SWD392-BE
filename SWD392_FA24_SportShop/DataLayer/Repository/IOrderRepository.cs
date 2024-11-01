using DataLayer.DTO;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IOrderRepository
    {
        Task<bool> CreateOrderAsync (Order order);
        Task<bool> UpdateOrderAsync (Order orderId);
        Task<bool> DeleteOrderAsync (string orderId);
        Task<Order> GetOrderByIdAsync (string orderId);
        Task<List<Order>> GetOrderByCurrentUser (int userId);
        Task<Order> GetCart(int userId);
        Task<List<Order>> GetOrderByUserIdAsync(int userId);
        Task<List<Order>> GetAllOrders();
        Task<bool> CalculatePriceAsync (string orderId);
        Task<bool> ProcessRefundAsync (string orderId);
        Task<bool> ChangeOrderStatusAsync (string orderId, int newStatus);
        //Task<List<OrderDto>> GetOrdersByUserIdAsync (int userId);
        //Task<List<OrderDetailDto>> GetOrderDetailsByOrderIdAsync (string orderId);
        Task<bool> AddReviewAsync(string orderId, int orderDetailId, int scoreRating, string comment);
        Task<bool> EditReviewAsync(int orderDetailId, int scoreRating, string comment);
        Task<bool> DeleteReviewAsync(int orderDetailId);
        Task<OrderDetail> GetReviewByOrderDetailIdAsync(int orderDetailId);

    }
}
