using BusinessLayer.ResponseModels;
using DataLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IDashboardService
    {
        Task<BaseResponse<AdminManagerDashboadDto>> GetDashboardForAdminManagerAsync();
        Task<BaseResponse<StaffDashboardDto>> GetDashboardForStaffAsync();
        Task<BaseResponse<UserDashboardDto>> GetDashboardForUserAsync(int userId);
    }
}
