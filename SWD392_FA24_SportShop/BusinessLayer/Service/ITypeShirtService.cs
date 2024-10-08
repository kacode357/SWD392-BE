using BusinessLayer.RequestModel.TypeShirt;
using BusinessLayer.ResponseModel.TypeShirt;
using BusinessLayer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface ITypeShirtService
    {
        Task<BaseResponse<TypeShirtResponseModel>> CreateTypeShirtAsync(CreateTypeShirtRequestModel model);
        Task<BaseResponse<TypeShirtResponseModel>> UpdateTypeShirtAsync(CreateTypeShirtRequestModel model, int id);
        Task<BaseResponse<TypeShirtResponseModel>> DeleteTypeShirtAsync(int typeShirtId, bool status);
        Task<BaseResponse<TypeShirtResponseModel>> GetTypeShirtById(int typeShirtId);
        Task<DynamicResponse<TypeShirtResponseModel>> GetAllTypeShirtAsync(GetAllTypeShirtRequestModel model);
    }
}
