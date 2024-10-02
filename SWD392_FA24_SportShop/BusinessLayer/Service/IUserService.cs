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
        public Task<BaseResponse<UserResponseModel>> RegisterUserByEmail(string googleId);
        public Task<BaseResponse<UserResponseModel>> CreateAccountAdmin(string email, string password, string name);
        public Task<BaseResponse<UserResponseModel>> CreateAccountStaff(string email, string password, string name);
        public Task<BaseResponse<UserResponseModel>> CreateAccountManager(string email, string password, string name);
        public Task<BaseResponse<UserResponseModel>> RegisterUser(RegisterRequestModel model);
        Task<BaseResponse> SendMailWithoutPassword(string email);
        Task<BaseResponse> SendMailWithPassword(string email, string password);
        Task<BaseResponse> VerifyAcccount(int id);
        Task<BaseResponse<LoginResponseModel>> Login(LoginRequestModel model);
        Task<BaseResponse<LoginResponseModel>> LoginMail(string googleId);
        Task<DynamicResponse<UserResponseModel>> GetListUser(GetAllUserRequestModel model);
        Task<BaseResponse<UserResponseModel>> GetUserById(int id);
        Task<BaseResponse<UserResponseModel>> UpdateUser(int id, UpdateRequestModel model);
        Task<BaseResponse<UserResponseModel>> DeleteUser(int id, bool status);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);

    }
}
