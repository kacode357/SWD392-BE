using AutoMapper;
using BusinessLayer.RequestModel.TypeShirt;
using BusinessLayer.ResponseModel.Session;
using BusinessLayer.ResponseModel.Shirt;
using BusinessLayer.ResponseModel.TypeShirt;
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
    public class TypeShirtService : ITypeShirtService
    {
        private readonly ITypeShirtRepository _typeShirtRepository;
        private readonly IMapper _mapper;
        public TypeShirtService(ITypeShirtRepository typeShirtRepository, IMapper mapper)
        {
            _typeShirtRepository = typeShirtRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<TypeShirtResponseModel>> CreateTypeShirtAsync(CreateTypeShirtRequestModel model)
        {
            try
            {
                var typeShirt = _mapper.Map<TypeShirt>(model);
                typeShirt.Status = true;
                await _typeShirtRepository.CreateTypeShirtAsync(typeShirt);
                return new BaseResponse<TypeShirtResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Create TypeShirt success!.",
                    Data = _mapper.Map<TypeShirtResponseModel>(typeShirt)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TypeShirtResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<TypeShirtResponseModel>> DeleteTypeShirtAsync(int typeShirtId, bool status)
        {
            try
            {
                var typeShirt = await _typeShirtRepository.GetTypeShirtById(typeShirtId);
                if (typeShirt == null)
                {
                    return new BaseResponse<TypeShirtResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found TypeShirt!.",
                        Data = null
                    };
                }
                typeShirt.Status = status;
                await _typeShirtRepository.UpdateTypeShirtAsync(typeShirt);
                return new BaseResponse<TypeShirtResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<TypeShirtResponseModel>(typeShirt)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TypeShirtResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<TypeShirtResponseModel>> GetAllTypeShirtAsync(GetAllTypeShirtRequestModel model)
        {
            try
            {
                var listTypeShirtDto = await _typeShirtRepository.GetAllTypeShirtAsync();

                
                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    listTypeShirtDto = listTypeShirtDto
                        .Where(c => c.Name.ToLower().Contains(model.keyWord.ToLower()))
                        .ToList();
                }

                
                if (model.Status != null)
                {
                    listTypeShirtDto = listTypeShirtDto
                        .Where(c => c.Status == model.Status)
                        .ToList();
                }

                
                var result = _mapper.Map<List<TypeShirtResponseModel>>(listTypeShirtDto);

                
                var pageTypeShirt = result
                    .OrderBy(c => c.Id)
                    .ToPagedList(model.pageNum, model.pageSize);

                return new DynamicResponse<TypeShirtResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = new MegaData<TypeShirtResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageTypeShirt.PageNumber,
                            Size = pageTypeShirt.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageTypeShirt.PageCount,
                            TotalItem = pageTypeShirt.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            status = model.Status,
                            is_Verify = null,
                            is_Delete = null
                        },
                        PageData = pageTypeShirt.ToList()
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<TypeShirtResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }

            /*try
            {
                var listTypeShirt = await _typeShirtRepository.GetAllTypeShirtAsync();

                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    List<TypeShirt> listTypeShirtByName = listTypeShirt.Where(c => c.Name.ToLower().Contains(model.keyWord)).ToList();

                    listTypeShirt = listTypeShirtByName
                               .GroupBy(c => c.Id)
                               .Select(g => g.First())
                               .ToList();
                }
                if (model.Status != null)
                {
                    listTypeShirt = listTypeShirt.Where(c => c.Status == model.Status).ToList();
                }
                var result = _mapper.Map<List<TypeShirtResponseModel>>(listTypeShirt);
                // Nếu không có lỗi, thực hiện phân trang
                var pageTypeShirt = result// Giả sử result là danh sách người dùng
                    .OrderBy(c => c.Id) // Sắp xếp theo Id tăng dần
                    .ToPagedList(model.pageNum, model.pageSize); // Phân trang với X.PagedList
                return new DynamicResponse<TypeShirtResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<TypeShirtResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageTypeShirt.PageNumber,
                            Size = pageTypeShirt.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageTypeShirt.PageCount,
                            TotalItem = pageTypeShirt.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            status = model.Status,
                            is_Verify = null,
                            is_Delete = null
                        },
                        PageData = pageTypeShirt.ToList()
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<TypeShirtResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }*/
        }

        public async Task<BaseResponse<TypeShirtResponseModel>> GetTypeShirtById(int typeShirtId)
        {
            try
            {
                var typeShirt = await _typeShirtRepository.GetTypeShirtById(typeShirtId);
                if (typeShirt == null)
                {
                    return new BaseResponse<TypeShirtResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found TypeShirt!",
                        Data = null
                    };
                }
                return new BaseResponse<TypeShirtResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<TypeShirtResponseModel>(typeShirt)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TypeShirtResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<TypeShirtResponseModel>> UpdateTypeShirtAsync(CreateTypeShirtRequestModel model, int id)
        {
            try
            {
                var typeShirt = await _typeShirtRepository.GetTypeShirtById(id);
                if (typeShirt == null)
                {
                    return new BaseResponse<TypeShirtResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found TypeShirt!.",
                        Data = null
                    };
                }
                await _typeShirtRepository.UpdateTypeShirtAsync(_mapper.Map(model, typeShirt));
                return new BaseResponse<TypeShirtResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update Session Success!.",
                    Data = _mapper.Map<TypeShirtResponseModel>(typeShirt)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TypeShirtResponseModel>()
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
