
using BusinessLayer.RequestModel.User;
using BusinessLayer.ResponseModel.User;
using BusinessLayer.ResponseModels;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace SWDProject_BE.Controllers
{
	[Route("API/User/")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly IUserService _service;

		public LoginController(IUserService services)
		{
            _service = services;
		}

        [HttpPost("VerifyEmail/{email}")]
        public async Task<BaseResponse> VerifyAccount(string email)
        {
            try
            {
                var result = await _service.VerifyAcccount(email);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<BaseResponse<RegisterResponseModel>> Register(RegisterRequestModel model)
        {
            try
            {
                var result = await _service.RegisterUser(model);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<BaseResponse<LoginResponseModel>> Login(LoginRequestModel model)
        {
            try
            {
                var result = await _service.Login(model);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("LoginMail")]
        public async Task<BaseResponse<LoginResponseModel>> LoginMail(string mail)
        {
            try
            {
                var result = await _service.LoginMail(mail);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetAllUser")]
        public async Task<BaseResponse<List<RegisterResponseModel>>> GetAllUser()
        {
            try
            {
                var result = await _service.GetListUser();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<BaseResponse<RegisterResponseModel>> GetUserById(int id)
        {
            try
            {
                var result = await _service.GetUserById(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<BaseResponse<RegisterResponseModel>> UpdateUser(int id, UpdateRequestModel model)
        {
            try
            {
                var result = await _service.UpdateUser(id, model);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("DeleteUser/{id}")]
        public async Task<BaseResponse<RegisterResponseModel>> DeleteUser(int id)
        {
            try
            {
                var result = await _service.DeleteUser(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
