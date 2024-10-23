using BusinessLayer.RequestModel.Shirt;
using BusinessLayer.Service;
using BusinessLayer.Service.Implement;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SWD392_SportShop.Controllers
{
    [Route("API/Shirt/")]
    [ApiController]
    public class ShirtController : ControllerBase
    {
        private readonly IShirtService _shirtService;

        public ShirtController(IShirtService shirtService)
        {
            _shirtService = shirtService;
        }
        [Authorize]
        [HttpPost("GetAllByName")]
        public async Task<IActionResult> GetAllByName(string name)
        {
            try
            {
                var result = await _shirtService.GetAllShirtsByName(name);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _shirtService.GetAllShirt();
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("Search")]
        public async Task<IActionResult> GetShirts(GetAllShirtRequestModel model)
        {
            try
            {
                var result = await _shirtService.GetShirtsAsync(model);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("SearchByMutilName")]
        public async Task<IActionResult> SearchByMutilName(SearchShirtByMutilName model)
        {
            try
            {
                var result = await _shirtService.SearchShirtBySessionClubPlayerShirtName(model);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShirtById(int id)
        {
            var result = await _shirtService.GetShirtById(id);
            return StatusCode(result.Code, result);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateShirt([FromBody] CreateShirtRequestModel model)
        {
            try
            {
                var result = await _shirtService.CreateShirtAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShirt(int id, [FromBody] CreateShirtRequestModel model)
        {
            try
            {
                var result = await _shirtService.UpdateShirtAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShirt(int id, int status)
        {
            try
            {
                var result = await _shirtService.DeleteShirtAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
