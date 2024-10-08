using AutoMapper;
using BusinessLayer.RequestModel.Shirt;
using BusinessLayer.ResponseModel.Session;
using BusinessLayer.ResponseModel.Shirt;
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
    public class ShirtService : IShirtService
    {
        private readonly IShirtRepository _shirtRepository;
        private readonly IMapper _mapper;

        public ShirtService(IShirtRepository shirtRepository, IMapper mapper)
        {
            _shirtRepository = shirtRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ShirtResponseModel>> CreateShirtAsync(CreateShirtRequestModel model)
        {
            try
            {
                var shirt = _mapper.Map<Shirt>(model);
                shirt.Status = 1;
                await _shirtRepository.CreateShirtAsync(shirt);
                return new BaseResponse<ShirtResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Create Shirt success!.",
                    Data = _mapper.Map<ShirtResponseModel>(shirt)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ShirtResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ShirtResponseModel>> DeleteShirtAsync(int shirtId, int status)
        {
            try
            {
                var shirt = await _shirtRepository.GetShirtById(shirtId);
                if (shirt == null)
                {
                    return new BaseResponse<ShirtResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Shirt!.",
                        Data = null
                    };
                }
                shirt.Status = status;
                await _shirtRepository.UpdateShirtAsync(shirt);
                return new BaseResponse<ShirtResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<ShirtResponseModel>(shirt)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ShirtResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ShirtResponseModel>> GetShirtById(int shirtId)
        {
            try
            {
                var shirt = await _shirtRepository.GetShirtById(shirtId);
                if (shirt == null)
                {
                    return new BaseResponse<ShirtResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Shirt!",
                        Data = null
                    };
                }
                return new BaseResponse<ShirtResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<ShirtResponseModel>(shirt)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ShirtResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<ShirtResponseModel>> GetShirtsAsync(GetAllShirtRequestModel model)
        {
            try
            {
                var listShirtDto = await _shirtRepository.GetAllShirts();

                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    listShirtDto = listShirtDto
                        .Where(c => c.Name.ToLower().Contains(model.keyWord.ToLower()))
                        .ToList();
                }
                if (model.Status.HasValue) 
                {
                    bool statusBool = model.Status.Value == 1; 
                    listShirtDto = listShirtDto
                        .Where(c => (c.Status == 1) == statusBool) 
                        .ToList();
                }
                var result = _mapper.Map<List<ShirtResponseModel>>(listShirtDto);
                // Nếu không có lỗi, thực hiện phân trang
                var pageShirt = result// Giả sử result là danh sách người dùng
                    .OrderBy(c => c.Id) // Sắp xếp theo Id tăng dần
                    .ToPagedList(model.pageNum, model.pageSize); // Phân trang với X.PagedList
                return new DynamicResponse<ShirtResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<ShirtResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageShirt.PageNumber,
                            Size = pageShirt.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageShirt.PageCount,
                            TotalItem = pageShirt.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            status = model.Status.HasValue ? (model.Status.Value == 1) : (bool?)null,
                            is_Verify = null,
                            is_Delete = null
                        },
                        PageData = pageShirt.ToList()
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<ShirtResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ShirtResponseModel>> UpdateShirtAsync(CreateShirtRequestModel model, int id)
        {
            try
            {
                var shirt = await _shirtRepository.GetShirtById(id);
                if (shirt == null)
                {
                    return new BaseResponse<ShirtResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Shirt!.",
                        Data = null
                    };
                }
                await _shirtRepository.UpdateShirtAsync(_mapper.Map(model, shirt));
                return new BaseResponse<ShirtResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update Shirt Success!.",
                    Data = _mapper.Map<ShirtResponseModel>(shirt)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ShirtResponseModel>()
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
