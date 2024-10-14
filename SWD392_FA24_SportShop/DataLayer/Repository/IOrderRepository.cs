﻿using DataLayer.DTO;
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
        Task<bool> DeleteOrderAsync (string orderId);
        Task<Order> GetOrderByIdAsync (string orderId);
        Task<List<OrderDto>> GetAllOrders();
        Task<bool> CalculatePriceAsync (string orderId);
        Task<bool> ProcessRefundAsync (string orderId);
        Task<bool> ChangeOrderStatusAsync (string orderId, int newStatus);
        Task<List<OrderDto>> GetOrdersByUserIdAsync (int userId);
        Task<List<OrderDetailDto>> GetOrderDetailsByOrderIdAsync (string orderId);
    }
}