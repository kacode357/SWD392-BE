using BusinessLayer.RequestModel.User;
using BusinessLayer.ResponseModel.User;
using BusinessLayer.ResponseModels;
using BusinessLayer.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IUserService
    {
        public Task<BaseResponse<LoginResponseModel>> RegisterUser(LoginRequestModel model);
        Task<BaseResponse> SendMailAccount(string email, string password);
        Task<BaseResponse> VerifyAcccount(string email);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);

    }
}
