using BusinessLayer.RequestModel.ShirtSize;
using BusinessLayer.RequestModel.Size;
using BusinessLayer.ResponseModel.ShirtSize;
using BusinessLayer.ResponseModel.Size;
using BusinessLayer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IShirtSizeService
    {
        Task<BaseResponse<ShirtSizeResponseModel>> CreateShirtSizeAsync(CreateShirtSizeRequestModel model);
        Task<BaseResponse<ShirtSizeResponseModel>> UpdateShirtSizeAsync(CreateShirtSizeRequestModel model, int id);
        Task<BaseResponse<ShirtSizeResponseModel>> DeleteShirtSizeAsync(int shirtSizeId, bool status);
        Task<BaseResponse<ShirtSizeResponseModel>> GetShirtSizeByIdAsync(int shirtSizeId);
        Task<DynamicResponse<ShirtSizeResponseModel>> GetAllShirtSizeAsync(GetAllShirtSizeRequestModel model);
    }
}
