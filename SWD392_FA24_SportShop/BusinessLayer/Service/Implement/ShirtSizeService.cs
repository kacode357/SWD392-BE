using AutoMapper;
using BusinessLayer.RequestModel.ShirtSize;
using BusinessLayer.ResponseModel.ShirtSize;
using BusinessLayer.ResponseModel.Size;
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
    public class ShirtSizeService : IShirtSizeService
    {
        private readonly IShirtSizeRepository _shirtSizeRepository;
        private readonly IMapper _mapper;

        public ShirtSizeService(IShirtSizeRepository shirtSizeRepository, IMapper mapper)
        {
            _shirtSizeRepository = shirtSizeRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ShirtSizeResponseModel>> CreateShirtSizeAsync(CreateShirtSizeRequestModel model)
        {
            try
            {
                var shirtSize = _mapper.Map<ShirtSize>(model);
                shirtSize.Status = true;
                await _shirtSizeRepository.CreateShirtSizeAsync(shirtSize);
                return new BaseResponse<ShirtSizeResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Create ShirtSize success!.",
                    Data = _mapper.Map<ShirtSizeResponseModel>(shirtSize)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ShirtSizeResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ShirtSizeResponseModel>> DeleteShirtSizeAsync(int shirtSizeId, bool status)
        {
            try
            {
                var shirtSize = await _shirtSizeRepository.GetShirtSizeByIdAsync(shirtSizeId);
                if (shirtSize == null)
                {
                    return new BaseResponse<ShirtSizeResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found ShirtSize!.",
                        Data = null
                    };
                }
                shirtSize.Status = status;
                await _shirtSizeRepository.UpdateShirtSizeAsync(shirtSize);
                return new BaseResponse<ShirtSizeResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<ShirtSizeResponseModel>(shirtSize)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ShirtSizeResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<ShirtSizeResponseModel>> GetAllShirtSizeAsync(GetAllShirtSizeRequestModel model)
        {
            try
            {
                var listShirtSize = await _shirtSizeRepository.GetAllShirtSizeAsync();

                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    List<ShirtSize> listShirtSizeByDescription = listShirtSize.Where(c => c.Description.ToLower().Contains(model.keyWord)).ToList();

                    listShirtSize = listShirtSizeByDescription
                               .GroupBy(c => c.Id)
                               .Select(g => g.First())
                               .ToList();
                }
                if (model.Status != null)
                {
                    listShirtSize = listShirtSize.Where(c => c.Status == model.Status).ToList();
                }
                var result = _mapper.Map<List<ShirtSizeResponseModel>>(listShirtSize);
                // Nếu không có lỗi, thực hiện phân trang
                var pageShirtSize = result// Giả sử result là danh sách người dùng
                    .OrderBy(c => c.Id) // Sắp xếp theo Id tăng dần
                    .ToPagedList(model.pageNum, model.pageSize); // Phân trang với X.PagedList
                return new DynamicResponse<ShirtSizeResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<ShirtSizeResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageShirtSize.PageNumber,
                            Size = pageShirtSize.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageShirtSize.PageCount,
                            TotalItem = pageShirtSize.TotalItemCount,
                        },
                        SearchInfo = null,
                        PageData = pageShirtSize.ToList()
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<ShirtSizeResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ShirtSizeResponseModel>> GetShirtSizeByIdAsync(int shirtSizeId)
        {
            try
            {
                var shirtSize = await _shirtSizeRepository.GetShirtSizeByIdAsync(shirtSizeId);
                if (shirtSize == null)
                {
                    return new BaseResponse<ShirtSizeResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found ShirtSize!",
                        Data = null
                    };
                }
                return new BaseResponse<ShirtSizeResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<ShirtSizeResponseModel>(shirtSize)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ShirtSizeResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ShirtSizeResponseModel>> UpdateShirtSizeAsync(CreateShirtSizeRequestModel model, int id)
        {
            try
            {
                var shirtSize = await _shirtSizeRepository.GetShirtSizeByIdAsync(id);
                if (shirtSize == null)
                {
                    return new BaseResponse<ShirtSizeResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found ShirtSize!.",
                        Data = null
                    };
                }
                await _shirtSizeRepository.UpdateShirtSizeAsync(_mapper.Map(model, shirtSize));
                return new BaseResponse<ShirtSizeResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update ShirtSize Success!.",
                    Data = _mapper.Map<ShirtSizeResponseModel>(shirtSize)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ShirtSizeResponseModel>()
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
