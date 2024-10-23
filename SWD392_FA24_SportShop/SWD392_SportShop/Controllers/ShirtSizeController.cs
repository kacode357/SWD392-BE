using BusinessLayer.RequestModel.Session;
using BusinessLayer.RequestModel.ShirtSize;
using BusinessLayer.Service;
using BusinessLayer.Service.Implement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SWD392_SportShop.Controllers
{
    [Route("API/ShirtSize/")]
    [ApiController]
    public class ShirtSizeController : ControllerBase
    {
        private readonly IShirtSizeService _shirtSizeService;
        public ShirtSizeController(IShirtSizeService shirtSizeService)
        {
            _shirtSizeService = shirtSizeService;
        }
        [Authorize]
        [HttpPost("Search")]
        public async Task<IActionResult> GetShirtSizes(GetAllShirtSizeRequestModel model)
        {
            try
            {
                var result = await _shirtSizeService.GetAllShirtSizeAsync(model);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShirtSizeById(int id)
        {
            try
            {
                var result = await _shirtSizeService.GetShirtSizeByIdAsync(id);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateShirtSize([FromBody] CreateShirtSizeRequestModel model)
        {
            try
            {
                var result = await _shirtSizeService.CreateShirtSizeAsync(model);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateShirtSize(int id,[FromBody] CreateShirtSizeRequestModel model)
        {
            try
            {
                var result = await _shirtSizeService.UpdateShirtSizeAsync(model, id);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete]
        public async Task<IActionResult> DeleteShirtSize(int id, bool status)
        {
            try
            {
                var result = await _shirtSizeService.DeleteShirtSizeAsync(id, status);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
