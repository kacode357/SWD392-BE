using AutoMapper.Configuration.Conventions;
using DataLayer.DTO;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IOrderDetailRepository
    {
        Task<bool> AddOrderDetailAsync (OrderDetail orderDetail);
        Task<bool> UpdateOrderDetailAsync(OrderDetail orderDetailId);
        Task<bool> DeleteOrderDetailAsync (int orderDetailId);
        Task<OrderDetail> GetOrderDetailByIdAsync(int orderDetailId);
        Task<OrderDetail> GetOrderDetailAsync(string orderId, int shirtId);
        Task<List<OrderDetailDto>> GetAllOrderDetails();
        Task<List<OrderDetail>> GetAllOrderDetailsByOrderId(string orderId);
    }
}
