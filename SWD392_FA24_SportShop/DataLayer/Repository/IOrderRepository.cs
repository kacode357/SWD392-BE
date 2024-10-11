using DataLayer.DTO;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IOrderRepository
    {
        Task<bool> CreateOrderAsync (Order order);
        Task<bool> UpdateOrderAsync (Order orderId);
        Task<bool> DeleteOrderAsync (int orderId);
        Task<Order> GetOrderByIdAsync (int orderId);
        Task<List<OrderDto>> GetAllOrders();
        Task<bool> CalculatePriceAsync (int orderId);
        Task<bool> ProcessRefundAsync (int orderId);
        Task<bool> ChangeOrderStatusAsync (int orderId, int newStatus);
        Task<List<OrderDto>> GetOrdersByUserIdAsync (int userId);
        Task<List<OrderDetailDto>> GetOrderDetailsByOrderIdAsync (int orderId);
    }
}
