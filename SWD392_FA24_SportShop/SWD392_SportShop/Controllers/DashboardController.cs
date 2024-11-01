using BusinessLayer.ResponseModels;
using BusinessLayer.Service;
using DataLayer.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Google.Apis.Requests.BatchRequest;

namespace SWD392_SportShop.Controllers
{
    [Route("API/Dashboard/")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("dashboard-admin-manager")]
        public async Task<ActionResult<BaseResponse<AdminManagerDashboadDto>>> GetDashboardForAdminManager()
        {
            try
            {
                var response = await _dashboardService.GetDashboardForAdminManagerAsync();

                if(response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(response.Code, response); 
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
            }
        }
        [Authorize(Roles = "User")]
        [HttpGet("dashboard-user")]
        public async Task<ActionResult<BaseResponse<UserDashboardDto>>> GetDashboardForUser()
        {
            try
            {
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdString, out int userId))
                {
                    var response = await _dashboardService.GetDashboardForUserAsync(userId);
                    return StatusCode(response.Code, response);
                }
                else
                {
                    return BadRequest(new { Message = "Invalid user ID." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
            }
        }
        [Authorize(Roles = "Staff")]
        [HttpGet("dashboard-staff")]
        public async Task<ActionResult<BaseResponse<UserDashboardDto>>> GetDashboardForStaff()
        {
            try
            {
                var response = await _dashboardService.GetDashboardForStaffAsync();
                return StatusCode(response.Code, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
            }
        }


    }
}
