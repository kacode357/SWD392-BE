using BusinessLayer.RequestModel.Club;
using BusinessLayer.ResponseModel.Club;
using BusinessLayer.ResponseModels;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IClubService
    {
        Task<BaseResponse<ClubResponseModel>> CreateClubAsync(CreateClubRequestModel model);
        Task<BaseResponse<ClubResponseModel>> UpdateClubAsync(CreateClubRequestModel model, int id);
        Task<BaseResponse<ClubResponseModel>> DeleteClubAsync(int clubId, bool status);
        Task<BaseResponse<ClubResponseModel>> GetClubById(int clubId);
        Task<DynamicResponse<ClubResponseModel>> GetAllClubs(GetAllClubRequestModel model);
    }
}
