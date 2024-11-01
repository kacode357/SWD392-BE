using BusinessLayer.ResponseModel.Club;
using BusinessLayer.ResponseModel.Order;
using BusinessLayer.ResponseModels;
using DataLayer.DBContext;
using DataLayer.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Implement
{
    public class DashboardService : IDashboardService
    {
        private readonly db_aad141_swd392Context _swd392Context;

        public DashboardService(db_aad141_swd392Context swd392Context)
        {
            _swd392Context = swd392Context;
        }

        public async Task<BaseResponse<AdminManagerDashboadDto>> GetDashboardForAdminManagerAsync()
        {
            try
            {
                var statistics = new AdminManagerDashboadDto
                {
                    UserCount = await _swd392Context.Users.CountAsync(),
                    ClubCount = await _swd392Context.Clubs.CountAsync(),
                    SessionCount = await _swd392Context.Sessions.CountAsync(),
                    PlayerCount = await _swd392Context.Players.CountAsync(),
                    ShirtCount = await _swd392Context.Shirts.CountAsync(),
                    TypeShirtCount = await _swd392Context.TypeShirts.CountAsync(),
                    OrderCount = await _swd392Context.Orders.CountAsync()
                };
                return new BaseResponse<AdminManagerDashboadDto>
                {
                    Code = 200,
                    Success = true,
                    Message = "Data fetched successfully",
                    Data = statistics
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AdminManagerDashboadDto>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<StaffDashboardDto>> GetDashboardForStaffAsync()
        {
            try
            {
                var totalOrders = await _swd392Context.Orders.CountAsync();
                var totalPayments = await _swd392Context.Payments.CountAsync();
                var totalSalesAmount = await _swd392Context.Orders
                    .Where(o => o.Status != 1 && o.Status != 6)
                    .SumAsync(o => o.TotalPrice) ?? 0;

                var staffDashboard = new StaffDashboardDto
                {
                    TotalOrders = totalOrders,
                    TotalPayments = totalPayments,
                    TotalSalesAmount = totalSalesAmount
                };

                return new BaseResponse<StaffDashboardDto>
                {
                    Code = 200,
                    Success = true,
                    Message = "Data fetched successfully",
                    Data = staffDashboard
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<StaffDashboardDto>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<UserDashboardDto>> GetDashboardForUserAsync(int userId)
        {
            try
            {
                if (userId == null)
                {
                    return new BaseResponse<UserDashboardDto>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "User not found!.",
                        Data = null
                    };
                }

                var orderCount = await _swd392Context.Orders
                    .CountAsync(o => o.UserId == userId);

                var paymentCount = await _swd392Context.Payments
                    .CountAsync(p => p.UserId == userId);

                var dashboardData = new UserDashboardDto
                {
                    OrderCount = orderCount,
                    PaymentCount = paymentCount
                };

                return new BaseResponse<UserDashboardDto>
                {
                    Code = 200,
                    Success = true,
                    Message = "Data fetched successfully",
                    Data = dashboardData
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserDashboardDto>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }
    }
}
