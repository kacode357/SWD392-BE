using BusinessLayer.Service;
using DataLayer.Entities;
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

        [HttpGet]
        public async Task<IActionResult> GetShirts()
        {
            var shirts = await _shirtService.GetShirtsAsync();
            return Ok(shirts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShirtById(int shirtId)
        {
            var shirt = await _shirtService.GetShirtById(shirtId);
            return Ok(shirt);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShirt([FromBody] Shirt shirt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdShirt = await _shirtService.CreateShirtAsync(shirt);
            return CreatedAtAction(nameof(GetShirts), new { id = createdShirt.Id }, createdShirt);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShirt(int id, [FromBody] Shirt shirt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingShirt = await _shirtService.GetShirtById(id);
            if (existingShirt == null)
            {
                return NotFound();
            }

            existingShirt.TypeShirt = shirt.TypeShirt;
            existingShirt.PlayerId = shirt.PlayerId;
            existingShirt.Name = shirt.Name;
            existingShirt.Number = shirt.Number;
            existingShirt.Price = shirt.Price;
            existingShirt.Date = shirt.Date;
            existingShirt.Description = shirt.Description;
            existingShirt.UrlImg = shirt.UrlImg;
            existingShirt.Status = shirt.Status;

            await _shirtService.UpdateShirtAsync(existingShirt);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShirt(int shirtId)
        {
            var isDeleted = await _shirtService.DeleteShirtAsync(shirtId);
            if (!isDeleted)
            {
                return NotFound(); 
            }
            return Ok(isDeleted);
        }
    }
}
