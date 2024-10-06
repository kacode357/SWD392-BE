using BusinessLayer.RequestModel.Player;
using BusinessLayer.ResponseModel.Player;
using BusinessLayer.ResponseModels;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IPlayerService
    {
        Task<BaseResponse<PlayerResponseModel>> CreatePlayerAsync(CreatePlayerRequestModel model);
        Task<BaseResponse<PlayerResponseModel>> UpdatePlayerAsync(CreatePlayerRequestModel model, int id);
        Task<BaseResponse<PlayerResponseModel>> DeletePlayerAsync(int playerId, bool status);
        Task<BaseResponse<PlayerResponseModel>> GetPlayerById(int playerId);
        Task<DynamicResponse<PlayerResponseModel>> GetPlayers(GetAllPlayerRequestModel model);
    }
}
