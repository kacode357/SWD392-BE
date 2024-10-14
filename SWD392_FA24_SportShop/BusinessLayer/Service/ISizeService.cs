using BusinessLayer.RequestModel.Size;
using BusinessLayer.ResponseModel.Size;
using BusinessLayer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface ISizeService
    {
        Task<BaseResponse<SizeResponseModel>> CreateSizeAsync(CreateSizeRequestModel model);
        Task<BaseResponse<SizeResponseModel>> UpdateSizeAsync(CreateSizeRequestModel model, int id);
        Task<BaseResponse<SizeResponseModel>> DeleteSizeAsync(int sizeId, bool status);
        Task<BaseResponse<SizeResponseModel>> GetSizeByIdAsync(int sizeId);
        Task<DynamicResponse<SizeResponseModel>> GetAllSizeAsync(GetAllSizeRequestModel model);
    }
}
