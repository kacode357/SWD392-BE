using BusinessLayer.RequestModel.Session;
using BusinessLayer.RequestModel.TypeShirt;
using BusinessLayer.Service;
using BusinessLayer.Service.Implement;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SWD392_SportShop.Controllers
{
    [Route("API/TypeShirt/")]
    [ApiController]
    public class TypeShirtController : ControllerBase
    {
        private readonly ITypeShirtService _typeShirtService;
        public TypeShirtController(ITypeShirtService typeShirtService)
        {
            _typeShirtService = typeShirtService;
        }

        [Authorize]
        [HttpPost("Search")]
        public async Task<IActionResult> GetAllTypeShirt(GetAllTypeShirtRequestModel model)
        {
            try
            {
                var result = await _typeShirtService.GetAllTypeShirtAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeShirtById(int id)
        {
            try
            {
                var result = await _typeShirtService.GetTypeShirtById(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateTypeShirt([FromBody] CreateTypeShirtRequestModel model)
        {
            try
            {
                var result = await _typeShirtService.CreateTypeShirtAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateTypeShirt(int id, [FromBody] CreateTypeShirtRequestModel model)
        {
            try
            {
                var result = await _typeShirtService.UpdateTypeShirtAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("Change-Status/{id}")]
        public async Task<IActionResult> DeleteTypeShirt(int id, bool status)
        {
            try
            {
                var result = await _typeShirtService.DeleteTypeShirtAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
