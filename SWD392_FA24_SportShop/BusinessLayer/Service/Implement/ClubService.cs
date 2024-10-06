using AutoMapper;
using BusinessLayer.RequestModel.Club;
using BusinessLayer.ResponseModel.Club;
using BusinessLayer.ResponseModel.User;
using BusinessLayer.ResponseModels;
using DataLayer.Entities;
using DataLayer.Repository;
using DataLayer.Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BusinessLayer.Service.Implement
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepository;
        private readonly IMapper _mapper;
        public ClubService(IClubRepository clubRepository, IMapper mapper)
        {
            _clubRepository = clubRepository;
            _mapper = mapper;
        }
        public async Task<BaseResponse<ClubResponseModel>> CreateClubAsync(CreateClubRequestModel model)
        {
            try
            {
                var club = _mapper.Map<Club>(model);
                club.Status = true;
                await _clubRepository.CreateClubAsync(club);
                return new BaseResponse<ClubResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Create Club success!.",
                    Data = _mapper.Map<ClubResponseModel>(club)
                };                
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClubResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null                
                };
            }
        }

        public async Task<BaseResponse<ClubResponseModel>> DeleteClubAsync(int clubId, bool status)
        {
            try
            {
                var club = await _clubRepository.GetClubById(clubId);
                if (club == null)
                {
                    return new BaseResponse<ClubResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Club!.",
                        Data = null
                    };
                }
                club.Status = status;
                await _clubRepository.UpdateClubAsync(club);
                return new BaseResponse<ClubResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<ClubResponseModel>(club)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClubResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<ClubResponseModel>> GetAllClubs(GetAllClubRequestModel model)
        {
            try
            {
                var listClub = await _clubRepository.GetAllClubs();

                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    List<Club> listClubByName = listClub.Where(c => c.Name.ToLower().Contains(model.keyWord)).ToList();

                    List<Club> listClubByCountry = listClub.Where(u => u.Country.ToLower().Contains(model.keyWord)).ToList();

                    listClub = listClubByName
                               .Concat(listClubByCountry)
                               .GroupBy(c => c.Id)
                               .Select(g => g.First())
                               .ToList();
                }
                if (model.Status != null)
                {
                    listClub = listClub.Where(c => c.Status == model.Status).ToList();
                }
                var result = _mapper.Map<List<ClubResponseModel>>(listClub);
                // Nếu không có lỗi, thực hiện phân trang
                var pageClub = result// Giả sử result là danh sách người dùng
                    .OrderBy(c => c.Id) // Sắp xếp theo Id tăng dần
                    .ToPagedList(model.pageNum, model.pageSize); // Phân trang với X.PagedList
                return new DynamicResponse<ClubResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<ClubResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageClub.PageNumber,
                            Size = pageClub.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageClub.PageCount,
                            TotalItem = pageClub.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            status = model.Status,
                            is_Verify = null,
                            is_Delete = null
                        },
                        PageData = pageClub.ToList()
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<ClubResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ClubResponseModel>> GetClubById(int clubId)
        {
            try
            {
                var club = await _clubRepository.GetClubById(clubId);
                if(club == null)
                {
                    return new BaseResponse<ClubResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Club!.",
                        Data = null                      
                    };
                }
                return new BaseResponse<ClubResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<ClubResponseModel>(club)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClubResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ClubResponseModel>> UpdateClubAsync(CreateClubRequestModel model, int id)
        {
            try
            {
                var club = await _clubRepository.GetClubById(id);
                if(club == null)
                {
                    return new BaseResponse<ClubResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not fount Club!.",
                        Data = null
                    };
                }
                await _clubRepository.UpdateClubAsync(_mapper.Map(model, club));
                return new BaseResponse<ClubResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update Club Success!.",
                    Data = _mapper.Map<ClubResponseModel>(club)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClubResponseModel>()
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
