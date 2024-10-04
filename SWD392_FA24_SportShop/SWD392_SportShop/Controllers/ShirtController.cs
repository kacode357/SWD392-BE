﻿using BusinessLayer.RequestModel.Shirt;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShirtById(int shirtId)
        {
            var result = await _shirtService.GetShirtById(shirtId);
            return StatusCode(result.Code, result);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShirt(int shirtId, int status)
        {
            try
            {
                var result = await _shirtService.DeleteShirtAsync(shirtId, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
