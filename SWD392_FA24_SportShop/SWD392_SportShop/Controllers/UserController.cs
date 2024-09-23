
using BusinessLayer.RequestModel.User;
using BusinessLayer.ResponseModel.User;
using BusinessLayer.ResponseModels;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Claims;

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
        [HttpPost("CreateAdminAccount")]
        public async Task<IActionResult> Register(string email, string password, string name)
        {
            try
            {
                var result = await _service.CreateAccountAdmin(email,password, name);
                if (result.Code == 409)
                {
                    return StatusCode(409, result);
                }
                else if (result.Code == 500)
                {
                    return StatusCode(500, result);
                }
                else
                {
                    return StatusCode(201, result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("RegisterEmail")]
        public async Task<IActionResult> RegisterEmail(string googleId)
        {
            try
            {
                var result = await _service.RegisterUserByEmail(googleId);
                if (result.Code == 401)
                {
                    return StatusCode(401, result);
                }
                else if (result.Code == 404)
                {
                    return NotFound(result);
                }
                else if (result.Code == 500)
                {
                    return StatusCode(500, result);
                }
                else
                {
                    return StatusCode(201, result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequestModel model)
        {
            try
            {
                var result = await _service.RegisterUser(model);
                if (result.Code == 409)
                {
                    return StatusCode(409, result);
                }
                else if (result.Code == 500)
                {
                    return StatusCode(500, result);
                }
                else
                {
                    return StatusCode(201, result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPatch("VerifyEmail/{email}")]
        public async Task<IActionResult> VerifyAccount(string email)
        {
            try
            {
                var result = await _service.VerifyAcccount(email);
                if (result.Code == 404)
                {
                    return NotFound(result);
                }
                else if (result.Code == 500)
                {
                    return StatusCode(500, result);
                }
                else
                {
                    return StatusCode(200, result);
                }
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
                if(result.Code == 401)
                {
                    return StatusCode(401, result);
                }
                else if(result.Code == 404)
                {
                    return NotFound(result);
                }else if(result.Code == 500)
                {
                    return StatusCode(500, result);
                }
                else
                {
                    return Ok(result);
                }
                
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
                if (result.Code == 401)
                {
                    return StatusCode(401, result);
                }
                else if (result.Code == 404)
                {
                    return NotFound(result);
                }
                else if (result.Code == 500)
                {
                    return StatusCode(500, result);
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            { 
                var result = await _service.GetListUser();
                if (result.Code == 500)
                {
                    return StatusCode(500, result);
                }
                else
                {
                    return StatusCode(200, result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var result = await _service.GetUserById(id);
                if (result.Code == 404)
                {
                    return NotFound(result);
                }
                else if (result.Code == 500)
                {
                    return StatusCode(500, result);
                }
                else
                {
                    return StatusCode(200, result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateRequestModel model)
        {
            try
            {
                var result = await _service.UpdateUser(id, model);
                if (result.Code == 404)
                {
                    return NotFound(result);
                }
                else if (result.Code == 500)
                {
                    return StatusCode(500, result);
                }
                else
                {
                    return StatusCode(200, result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _service.DeleteUser(id);
                if (result.Code == 409)
                {
                    return StatusCode(401, result);
                }
                else if (result.Code == 404)
                {
                    return NotFound(result);
                }
                else if (result.Code == 500)
                {
                    return StatusCode(500, result);
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("GetCurrentUser")]
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
                    if (result.Code == 404)
                    {
                        return NotFound(result);
                    }
                    else if (result.Code == 500)
                    {
                        return StatusCode(500, result);
                    }
                    else
                    {
                        return StatusCode(200, result);
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
