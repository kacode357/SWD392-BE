using BusinessLayer.RequestModel.Size;
using BusinessLayer.Service;
using BusinessLayer.Service.Implement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace SWD392_SportShop.Controllers
{
    [Route("API/Size")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;
        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpPost("Search")]
        public async Task<IActionResult> GetSizes(GetAllSizeRequestModel model)
        {
            try
            {
                var result = await _sizeService.GetAllSizeAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSizeById(int id)
        {
            try
            {
                var result = await _sizeService.GetSizeByIdAsync(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateSize([FromBody] CreateSizeRequestModel model)
        {
            try
            {
                var result = await _sizeService.CreateSizeAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateSize(int id, [FromBody] CreateSizeRequestModel model)
        {
            try
            {
                var result = await _sizeService.UpdateSizeAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("Change-Status/{id}")]
        public async Task<IActionResult> DeleteSize(int id, bool status)
        {
            try
            {
                var result = await _sizeService.DeleteSizeAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
