using AutoMapper;
using BusinessLayer.RequestModel.Player;
using BusinessLayer.ResponseModel.Club;
using BusinessLayer.ResponseModel.Player;
using BusinessLayer.ResponseModels;
using DataLayer.Entities;
using DataLayer.Repository;
using DataLayer.Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BusinessLayer.Service.Implement
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        public PlayerService(IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<PlayerResponseModel>> CreatePlayerAsync(CreatePlayerRequestModel model)
        {
            try
            {
                var player = _mapper.Map<Player>(model);
                await _playerRepository.CreatePlayerAsync(player);
                return new BaseResponse<PlayerResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Create Player success!.",
                    Data = _mapper.Map<PlayerResponseModel>(player)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PlayerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<PlayerResponseModel>> DeletePlayerAsync(int playerId, bool status)
        {
            try
            {
                var player = await _playerRepository.GetPlayerById(playerId);
                if (player == null)
                {
                    return new BaseResponse<PlayerResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Player!.",
                        Data = null
                    };
                }
                player.Status = status;
                await _playerRepository.UpdatePlayerAsync(player);
                return new BaseResponse<PlayerResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<PlayerResponseModel>(player)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PlayerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<PlayerResponseModel>> GetPlayerById(int playerId)
        {
            try
            {
                var player = await _playerRepository.GetPlayerById(playerId);
                if (player == null)
                {
                    return new BaseResponse<PlayerResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Player!.",
                        Data = null
                    };
                }
                return new BaseResponse<PlayerResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<PlayerResponseModel>(player)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PlayerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<PlayerResponseModel>> GetPlayers(GetAllPlayerRequestModel model)
        {
            try
            {
                var listPlayer = await _playerRepository.GetPlayers();

                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    List<Player> listPlayerByName = listPlayer.Where(c => c.FullName.ToLower().Contains(model.keyWord)).ToList();


                    listPlayer = listPlayerByName
                               .GroupBy(c => c.Id)
                               .Select(g => g.First())
                               .ToList();
                }
                
                var result = _mapper.Map<List<PlayerResponseModel>>(listPlayer);
                // Nếu không có lỗi, thực hiện phân trang
                var pagePlayer = result// Giả sử result là danh sách người dùng
                    .OrderBy(c => c.Id) // Sắp xếp theo Id tăng dần
                    .ToPagedList(model.pageNum, model.pageSize); // Phân trang với X.PagedList
                return new DynamicResponse<PlayerResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<PlayerResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pagePlayer.PageNumber,
                            Size = pagePlayer.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pagePlayer.PageCount,
                            TotalItem = pagePlayer.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            status = model.Status,
                            is_Verify = null,
                            is_Delete = null
                        },
                        PageData = pagePlayer.ToList()
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<PlayerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<PlayerResponseModel>> UpdatePlayerAsync(CreatePlayerRequestModel model, int id)
        {
            try
            {
                var player = await _playerRepository.GetPlayerById(id);
                if (player == null)
                {
                    return new BaseResponse<PlayerResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not fount Player!.",
                        Data = null
                    };
                }
                await _playerRepository.UpdatePlayerAsync(_mapper.Map(model, player));
                return new BaseResponse<PlayerResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update Player Success!.",
                    Data = _mapper.Map<PlayerResponseModel>(player)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PlayerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }
    }
}
