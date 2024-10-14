using AutoMapper;
using BusinessLayer.RequestModel.Size;
using BusinessLayer.ResponseModel.Session;
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
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly IMapper _mapper;

        public SizeService(ISizeRepository sizeRepository, IMapper mapper)
        {
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }
        public async Task<BaseResponse<SizeResponseModel>> CreateSizeAsync(CreateSizeRequestModel model)
        {
            try
            {
                var size = _mapper.Map<Size>(model);
                size.Status = true;
                await _sizeRepository.CreateSizeAsync(size);
                return new BaseResponse<SizeResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Create Size success!.",
                    Data = _mapper.Map<SizeResponseModel>(size)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<SizeResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<SizeResponseModel>> DeleteSizeAsync(int sizeId, bool status)
        {
            try
            {
                var size = await _sizeRepository.GetSizeByIdAsync(sizeId);
                if (size == null)
                {
                    return new BaseResponse<SizeResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Size!.",
                        Data = null
                    };
                }
                size.Status = status;
                await _sizeRepository.UpdateSizeAsync(size);
                return new BaseResponse<SizeResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<SizeResponseModel>(size)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<SizeResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<SizeResponseModel>> GetAllSizeAsync(GetAllSizeRequestModel model)
        {
            try
            {
                var listSize = await _sizeRepository.GetAllSizesAsync();

                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    List<Size> listSizeByName = listSize.Where(c => c.Name.ToLower().Contains(model.keyWord)).ToList();

                    listSize = listSizeByName
                               .GroupBy(c => c.Id)
                               .Select(g => g.First())
                               .ToList();
                }
                if (model.Status != null)
                {
                    listSize = listSize.Where(c => c.Status == model.Status).ToList();
                }
                var result = _mapper.Map<List<SizeResponseModel>>(listSize);
                // Nếu không có lỗi, thực hiện phân trang
                var pageSize = result// Giả sử result là danh sách người dùng
                    .OrderBy(c => c.Id) // Sắp xếp theo Id tăng dần
                    .ToPagedList(model.pageNum, model.pageSize); // Phân trang với X.PagedList
                return new DynamicResponse<SizeResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<SizeResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageSize.PageNumber,
                            Size = pageSize.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageSize.PageCount,
                            TotalItem = pageSize.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            status = model.Status,
                            is_Verify = null,
                            is_Delete = null
                        },
                        PageData = pageSize.ToList()
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<SizeResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<SizeResponseModel>> GetSizeByIdAsync(int sizeId)
        {
            try
            {
                var size = await _sizeRepository.GetSizeByIdAsync(sizeId);
                if (size == null)
                {
                    return new BaseResponse<SizeResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Size!",
                        Data = null
                    };
                }
                return new BaseResponse<SizeResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<SizeResponseModel>(size)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<SizeResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<SizeResponseModel>> UpdateSizeAsync(CreateSizeRequestModel model, int id)
        {
            try
            {
                var size = await _sizeRepository.GetSizeByIdAsync(id);
                if (size == null)
                {
                    return new BaseResponse<SizeResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Size!.",
                        Data = null
                    };
                }
                await _sizeRepository.UpdateSizeAsync(_mapper.Map(model, size));
                return new BaseResponse<SizeResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update Size Success!.",
                    Data = _mapper.Map<SizeResponseModel>(size)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<SizeResponseModel>()
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
