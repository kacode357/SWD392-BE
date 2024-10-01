using BusinessLayer.Service;
using DataLayer.Entities;
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

        [HttpGet]
        public async Task<IActionResult> GetSessions()
        {
            var sessions = await _sessionService.GetSessions();
            return Ok(sessions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSessionById(int id)
        {
            var session = await _sessionService.GetSessionById(id);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(session);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession([FromBody] Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdSession = await _sessionService.CreateSessionAsync(session);
            return CreatedAtAction(nameof(GetSessionById), new { id = createdSession.Id }, createdSession);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSession(int id, [FromBody] Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingSession = await _sessionService.GetSessionById(id);
            if (existingSession == null)
            {
                return NotFound();
            }

            existingSession.Name = session.Name;
            existingSession.StartDdate = session.StartDdate;
            existingSession.EndDdate = session.EndDdate;
            existingSession.Description = session.Description;
            existingSession.Status = session.Status;

            await _sessionService.UpdateSessionAsync(existingSession);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var isDeleted = await _sessionService.DeleteSessionAsync(id); 

            if (!isDeleted)
            {
                return NotFound(); 
            }

            return Ok(new { success = isDeleted }); 
        }
    }
}
