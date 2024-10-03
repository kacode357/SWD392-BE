﻿using BusinessLayer.RequestModel.Club;
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
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpPost("Change-Status/{id}")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            try
            {
                var result = await _clubService.DeleteClubAsync(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
