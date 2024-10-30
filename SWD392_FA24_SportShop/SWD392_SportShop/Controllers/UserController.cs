
using BusinessLayer.RequestModel.User;
using BusinessLayer.ResponseModel.User;
using BusinessLayer.ResponseModels;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Claims;
using X.PagedList;

namespace SWDProject_BE.Controllers
{
	[Route("API/User/")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _service;

		public UserController(IUserService services)
		{
            _service = services;
		}
        [HttpPost("Admin")]
        public async Task<IActionResult> RegisterAdmin(string email, string password, string name)
        {
            try
            {
                var result = await _service.CreateAccountAdmin(email,password, name);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Staff")]
        public async Task<IActionResult> RegisterStaff(string email, string password, string name)
        {
            try
            {
                var result = await _service.CreateAccountStaff(email, password, name);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Manager")]
        public async Task<IActionResult> RegisterManager(string email, string password, string name)
        {
            try
            {
                var result = await _service.CreateAccountManager(email, password, name);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("Email")]
        public async Task<IActionResult> RegisterEmail(string googleId)
        {
            try
            {
                var result = await _service.RegisterUserByEmail(googleId);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Register(RegisterRequestModel model)
        {
            try
            {
                var result = await _service.RegisterUser(model);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("Verify/{id}")]
        public async Task<IActionResult> VerifyAccount(int id)
        {
            try
            {
                var result = await _service.VerifyAcccount(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("block/{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            try
            {
                var result = await _service.BlockUser(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            try
            {
                var result = await _service.Login(model);
                return StatusCode(result.Code, result);          
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("LoginMail")]
        public async Task<IActionResult> LoginMail(string googleId)
        {
            try
            {
                var result = await _service.LoginMail(googleId);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Search")]
        public async Task<IActionResult> GetAllUser(GetAllUserRequestModel model)
        {
            try
            {
                var result = await _service.GetListUser(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var result = await _service.GetUserById(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateRequestModel model)
        {
            try
            {
                var result = await _service.UpdateUser(id, model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Change-Status/{id}")]
        public async Task<IActionResult> DeleteUser(int id, bool status)
        {
            try
            {
                var result = await _service.DeleteUser(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("Current-User")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return StatusCode(401, new BaseResponse()
                    {
                        Code = 401,
                        Success = false,
                        Message = "User information not found, user may not be authenticated into the system!."
                    });
                }
                else
                {
                    var result = await _service.GetUserById(int.Parse(userId));
                    return StatusCode(result.Code, result);
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("Change-Password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestModel model)
        {
            try
            {
                // Get userId form JWT
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null)
                {
                    return StatusCode(401, new BaseResponse()
                    {
                        Code = 401,
                        Success = false,
                        Message = "User information not found, user may not be authenticated into the system!"
                    });
                }

                int userId = int.Parse(userIdClaim);

                //Call ChangePassword service
                var result = await _service.ChangePassword(userId, model.CurrentPassword, model.NewPassword);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpPost("Resend-Verification")]
        public async Task<IActionResult> ResendVerificationEmail([FromBody] string email)
        {
            try
            {
                var result = await _service.ResendVerificationEmail(email);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }



    }
}
