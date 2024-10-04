using BusinessLayer.RequestModel.Shirt;
using BusinessLayer.ResponseModel.Shirt;
using BusinessLayer.ResponseModels;
using DataLayer.Entities;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IShirtService
    {
        Task<BaseResponse<ShirtResponseModel>> CreateShirtAsync(CreateShirtRequestModel model);
        Task<DynamicResponse<ShirtResponseModel>> GetShirtsAsync(GetAllShirtRequestModel model);
        Task<BaseResponse<ShirtResponseModel>> UpdateShirtAsync(CreateShirtRequestModel model, int id);
        Task<BaseResponse<ShirtResponseModel>> DeleteShirtAsync(int shirtId, int status);
        Task<BaseResponse<ShirtResponseModel>> GetShirtById(int shirtId);
    }
}
