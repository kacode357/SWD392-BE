
using BusinessLayer.RequestModel.User;
using BusinessLayer.ResponseModel.User;
using BusinessLayer.ResponseModels;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<BaseResponse<LoginResponseModel>> Register(LoginRequestModel model)
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
    }
}
