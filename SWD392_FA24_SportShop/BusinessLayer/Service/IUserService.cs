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
        public Task<BaseResponse<RegisterResponseModel>> RegisterUser(RegisterRequestModel model);
        Task<BaseResponse> SendMailAccount(string email, string password);
        Task<BaseResponse> VerifyAcccount(string email);
        Task<BaseResponse<LoginResponseModel>> Login(LoginRequestModel model);
        Task<BaseResponse<LoginResponseModel>> LoginMail(string mail);
        Task<BaseResponse<List<RegisterResponseModel>>> GetListUser();
        Task<BaseResponse<RegisterResponseModel>> GetUserById(int id);
        Task<BaseResponse<RegisterResponseModel>> UpdateUser(int id, UpdateRequestModel model);
        Task<BaseResponse<RegisterResponseModel>> DeleteUser(int id);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);

    }
}
