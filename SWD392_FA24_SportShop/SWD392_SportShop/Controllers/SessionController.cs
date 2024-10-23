using BusinessLayer.RequestModel.Session;
using BusinessLayer.Service;
using BusinessLayer.Service.Implement;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SWD392_SportShop.Controllers
{
    [Route("API/Session/")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        [Authorize]
        [HttpPost("Search")]
        public async Task<IActionResult> GetSessions(GetAllSessionRequestModel model)
        {
            try
            {
                var result = await _sessionService.GetSessions(model);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSessionById(int id)
        {
            try
            {
                var result = await _sessionService.GetSessionById(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateSession([FromBody] CreateSessionRequestModel model)
        {
            try
            {
                var result = await _sessionService.CreateSessionAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateSession(int id, [FromBody] CreateSessionRequestModel model)
        {
            try
            {
                var result = await _sessionService.UpdateSessionAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("Change-Status/{id}")]
        public async Task<IActionResult> DeleteSession(int id, bool status)
        {
            try
            {
                var result = await _sessionService.DeleteSessionAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
