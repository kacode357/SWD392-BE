using BusinessLayer.RequestModel.Session;
using BusinessLayer.ResponseModel.Session;
using BusinessLayer.ResponseModels;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface ISessionService
    {
        Task<BaseResponse<SessionResponseModel>> CreateSessionAsync(CreateSessionRequestModel model);
        Task<BaseResponse<SessionResponseModel>> UpdateSessionAsync(CreateSessionRequestModel model, int id);
        Task<BaseResponse<SessionResponseModel>> DeleteSessionAsync(int sessionId, bool status);
        Task<BaseResponse<SessionResponseModel>> GetSessionById(int sessionId);
        Task<DynamicResponse<SessionResponseModel>> GetSessions(GetAllSessionRequestModel model);
    }
}
