using DataLayer.DBContext;
using DataLayer.DTO;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Implement
{
    public class OrderDetailRepository: IOrderDetailRepository
    {
        private readonly db_aad141_swd392Context _swd392Context;

        public OrderDetailRepository(db_aad141_swd392Context swd392Context)
        {
            _swd392Context = swd392Context;
        }

        public async Task<bool> AddOrderDetailAsync(OrderDetail orderDetail)
        {
            try
            {
                _swd392Context.OrderDetails.AddAsync(orderDetail);
                await _swd392Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteOrderDetailAsync(int orderDetailId)
        {
            try
            {
                var orderDetail = await _swd392Context.OrderDetails.FindAsync(orderDetailId);
                if (orderDetail == null)
                {
                    return false;
                }
                _swd392Context.OrderDetails.Remove(orderDetail);
                return await _swd392Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<List<OrderDetailDto>> GetAllOrderDetails()
        //{
        //    try
        //    {
        //        var query = from orderDetail in _swd392Context.OrderDetails
        //                    join order in _swd392Context.Orders on orderDetail.OrderId equals order.Id
        //                    join ShirtSize in _swd392Context.ShirtSizes on orderDetail.ShirtSizeId equals ShirtSize.Id
        //                    select new OrderDetailDto
        //                    {
        //                        Id = orderDetail.Id,
        //                        OrderId = orderDetail.OrderId,
        //                        ShirtId = shirt.Id,
        //                        ShirtName = shirt.Name,
        //                        Price = shirt.Price,
        //                        Quantity = orderDetail.Quantity,
        //                        Comment = orderDetail.Comment,
        //                        Score = orderDetail.Score,
        //                        StatusRating = orderDetail.StatusRating,
        //                        Status = orderDetail.Status,
        //                    };
        //        return await query.ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<List<OrderDetail>> GetAllOrderDetailsByOrderId(string orderId)
        {
            try
            {
                return await _swd392Context.OrderDetails
                    .Include(od => od.ShirtSize)
                        .ThenInclude(ss => ss.Shirt)
                    .Include(od => od.ShirtSize)
                        .ThenInclude(ss => ss.Size)
                    .Where(od => od.OrderId == orderId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OrderDetail> GetOrderDetailAsync(string orderId, int shirtSizeId)
        {
            try
            {
                return await _swd392Context.OrderDetails.Where(od => od.OrderId == orderId && od.ShirtSizeId == shirtSizeId).OrderByDescending(od => od.Id).FirstOrDefaultAsync();
            }
            catch (Exception ex) 
            {
                throw ex;
            }  
        }

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int orderDetailId)
        {
            try
            {
                return await _swd392Context.OrderDetails.FindAsync(orderDetailId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateOrderDetailAsync(OrderDetail orderDetailId)
        {
            try
            {
                _swd392Context.OrderDetails.Update(orderDetailId);
                await _swd392Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Not found!" + ex);
            }
        }
    }
}
