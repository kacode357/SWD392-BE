using BusinessLayer.RequestModel.Order;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SWD392_SportShop.Controllers
{
    [Route("API/Review/")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public ReviewController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> AddReview([FromBody] AddReviewRequestModel model)
        {
            try
            {
                var result = await _orderService.AddReviewAsync(model);
                if (!result.Success)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{orderDetailId}")]
        public async Task<IActionResult> EditReview(int orderDetailId, int scoreRating, string comment)
        {
            var result = await _orderService.EditReviewAsync(orderDetailId, scoreRating, comment);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [Authorize]
        [HttpDelete("{orderDetailId}")]
        public async Task<IActionResult> DeleteReview(int orderDetailId)
        {
            var result = await _orderService.DeleteReviewAsync(orderDetailId);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [Authorize]
        [HttpGet("{orderDetailId}")]
        public async Task<IActionResult> GetReviewByOrderDetailId(int orderDetailId)
        {
            var result = await _orderService.GetReviewByOrderDetailIdAsync(orderDetailId);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result);
        }
        [HttpGet("{shirtId}")]
        public async Task<IActionResult> GetReviewsByShirtId(int shirtId)
        {
            try
            {
                var result = await _orderService.GetReviewsByShirtIdAsync(shirtId);
                if (!result.Success)
                {
                    return NotFound(result.Message);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred: " + ex.Message });
            }
        }
    }
}
