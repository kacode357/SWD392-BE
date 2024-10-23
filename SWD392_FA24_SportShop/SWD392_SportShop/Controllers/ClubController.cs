using BusinessLayer.RequestModel.Club;
using BusinessLayer.Service;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SWD392_SportShop.Controllers
{
    [Route("API/Club/")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly IClubService _clubService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }
        [Authorize]
        [HttpPost("Search")]
        public async Task<IActionResult> GetAllClubs(GetAllClubRequestModel model)
        {
            try
            {
                var result = await _clubService.GetAllClubs(model);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClubById(int id)
        {
            try
            {
                var result = await _clubService.GetClubById(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateClub(CreateClubRequestModel model)
        {
            try
            {
                var result = await _clubService.CreateClubAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateClub([FromBody]CreateClubRequestModel model, int id)
        {
            try
            {
                var result = await _clubService.UpdateClubAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("Change-Status/{id}")]
        public async Task<IActionResult> DeleteClub(int id, bool status)
        {
            try
            {
                var result = await _clubService.DeleteClubAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClubAsync(int id, bool status)
        {
            try
            {
                var result = await _clubService.DeleteClubAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
