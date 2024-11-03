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
    public class OrderRepository : IOrderRepository
    {
        private readonly db_aad141_swd392Context _swd392Context;

        public OrderRepository(db_aad141_swd392Context swd392Context)
        {
            _swd392Context = swd392Context;
        }

        public async Task<bool> CalculatePriceAsync(string orderId)
        {
            try
            {
                var order = await _swd392Context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.Id == orderId);

                if (order == null || order.OrderDetails == null)
                {
                    return false;
                }

                order.TotalPrice = order.OrderDetails.Sum(od => od.Quantity * od.Price);
                _swd392Context.Update(order);
                return await _swd392Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<bool> ChangeOrderStatusAsync(string orderId, int newStatus)
        {
            var order = await _swd392Context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return false;
            }
            order.Status = newStatus;
            _swd392Context.Orders.Update(order);
            return await _swd392Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateOrderAsync(Order order)
        {
            try
            {
                _swd392Context.Orders.AddAsync(order);
                await _swd392Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteOrderAsync(string orderId)
        {
            try
            {
                var Order = await _swd392Context.Orders.FindAsync(orderId);
                if (Order == null)
                {
                    return false;
                }
                _swd392Context.Orders.Remove(Order);
                return await _swd392Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<List<OrderDto>> GetAllOrderByStatus()
        //{
        //    try
        //    {
        //        var query = from order in _swd392Context.Orders
        //                    join user in _swd392Context.Users on order.UserId equals user.Id
        //                    where order.Status != 1
        //                    select new OrderDto
        //                    {
        //                        Id = order.Id,
        //                        UserId = user.Id,
        //                        FullName = user.UserName,
        //                        TotalPrice = order.TotalPrice,
        //                        ShipPrice = order.ShipPrice,
        //                        Deposit = order.Deposit,
        //                        Date = order.Date,
        //                        RefundStatus = order.RefundStatus,
        //                        Status = order.Status,
        //                    };
        //        return await query.ToListAsync();
        //    } 
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<List<Order>> GetAllOrders()
        {
            try
            {
                return await _swd392Context.Orders
                    .Include(o => o.User)
                    .OrderByDescending(o => int.Parse(o.Id))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Order> GetCart(int userId)
        {
            try
            {
                return await _swd392Context.Orders.Where(o => o.UserId == userId && o.Status == 1).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Order> GetOrderByIdAsync(string orderId)
        {
            try
            {
                return await _swd392Context.Orders
                    .Include(o => o.OrderDetails)
                    .FirstOrDefaultAsync(o => o.Id == orderId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Order>> GetOrderByUserIdAsync(int userId)
        {
            try
            {
                return await _swd392Context.Orders.Where(o => o.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<List<OrderDetailDto>> GetOrderDetailsByOrderIdAsync(string orderId)
        //{
        //    try
        //    {
        //        var query = from orderDetail in _swd392Context.OrderDetails
        //                    join order in _swd392Context.Orders on orderDetail.OrderId equals orderId
        //                    select new OrderDetailDto
        //                    {
        //                        ShirtId = orderDetail.ShirtId,
        //                        ShirtName = orderDetail.Shirt.Name,
        //                        Quantity = orderDetail.Quantity,
        //                        Price = orderDetail.Price,
        //                        StatusRating = orderDetail.StatusRating,
        //                        Score = orderDetail.Score,
        //                        Status = orderDetail.Status,
        //                        Comment = orderDetail.Comment,
        //                    };
        //        return await query.ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<List<OrderDto>> GetOrdersByUserIdAsync(int userId)
        //{
        //    try
        //    {
        //        return await _swd392Context.Orders.Where(o => o.UserId == userId).Select(o => new OrderDto
        //        {
        //            Id = o.Id,
        //            UserId = userId,
        //            TotalPrice = o.TotalPrice,
        //            ShipPrice = o.ShipPrice,
        //            Deposit = o.Deposit,
        //            Date = o.Date,
        //            Status = o.Status,
        //        }).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<bool> ProcessRefundAsync(string orderId)
        {
            try
            {
                var order = await _swd392Context.Orders.FindAsync(orderId);
                if (order == null || order.RefundStatus)
                {
                    return false;
                }
                order.RefundStatus = true;
                _swd392Context.Orders.Update(order);
                return await _swd392Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateOrderAsync(Order orderId)
        {
            try
            {
                _swd392Context.Orders.Update(orderId);
                await _swd392Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Order>> GetOrderByCurrentUser(int userId)
        {
            try
            {
                return await _swd392Context.Orders
                    .Where(o => o.UserId == userId)
                    .Include(o => o.User)
                    .Include(o => o.OrderDetails)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AddReviewAsync(string orderId, int orderDetailId, int scoreRating, string comment)
        {
            try
            {
                var order = await _swd392Context.Orders
                    .Include(o => o.OrderDetails)
                    .FirstOrDefaultAsync(o => o.Id == orderId && o.Status != 1 && o.Status != 6);
                
                if (order == null)
                {
                    return false;
                }

                var orderDetail = order.OrderDetails.FirstOrDefault(od => od.Id == orderDetailId);

                if (orderDetail == null)
                {
                    return false;
                }

                orderDetail.Score = scoreRating;
                orderDetail.Comment = comment;

                _swd392Context.OrderDetails.Update(orderDetail);
                return await _swd392Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> EditReviewAsync(int orderDetailId, int scoreRating, string comment)
        {
            try
            {
                var orderDetail = await _swd392Context.OrderDetails.FindAsync(orderDetailId);
                if (orderDetail == null)
                {
                    return false;
                }

                orderDetail.Score = scoreRating;
                orderDetail.Comment = comment;

                _swd392Context.OrderDetails.Update(orderDetail);
                return await _swd392Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteReviewAsync(int orderDetailId)
        {
            try
            {
                var orderDetail = await _swd392Context.OrderDetails.FindAsync(orderDetailId);
                if (orderDetail == null)
                {
                    return false;
                }

                orderDetail.Score = null;
                orderDetail.Comment = null;

                _swd392Context.OrderDetails.Update(orderDetail);
                return await _swd392Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OrderDetail> GetReviewByOrderDetailIdAsync(int orderDetailId)
        {
            try
            {
                return await _swd392Context.OrderDetails
                    .FirstOrDefaultAsync(od => od.Id == orderDetailId && od.Score != null && od.Comment != null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
