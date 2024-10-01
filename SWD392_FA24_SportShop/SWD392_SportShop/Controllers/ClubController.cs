using BusinessLayer.Service;
using DataLayer.Entities;
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

        [HttpGet]
        public async Task<IActionResult> GetClubs()
        {
            var clubs = await _clubService.GetAllClubs();
            return Ok(clubs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClubById(int id)
        {
            var club = await _clubService.GetClubById(id);
            if (club == null)
            {
                return NotFound();
            }
            return Ok(club);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClub([FromBody] Club club)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdClub = await _clubService.CreateClubAsync(club);
            return CreatedAtAction(nameof(GetClubById), new { id = createdClub.Id }, createdClub);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClub(int id, [FromBody] Club club)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingClub = await _clubService.GetClubById(id);
            if (existingClub == null)
            {
                return NotFound();
            }

            existingClub.Name = club.Name;
            existingClub.Country = club.Country;
            existingClub.EstablishedYear = club.EstablishedYear;
            existingClub.StadiumName = club.StadiumName;
            existingClub.ClubLogo = club.ClubLogo;
            existingClub.Description = club.Description;
            existingClub.Status = club.Status;

            await _clubService.UpdateClubAsync(existingClub);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var isDeleted = await _clubService.DeleteClubAsync(id); 

            if (!isDeleted)
            {
                return NotFound(); 
            }

            return Ok(new { success = isDeleted }); 
        }
    }
}
