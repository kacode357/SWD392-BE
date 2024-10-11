using AutoMapper;
using BusinessLayer.RequestModel.OrderDetail;
using BusinessLayer.ResponseModel.Club;
using BusinessLayer.ResponseModel.Order;
using BusinessLayer.ResponseModel.OrderDetail;
using BusinessLayer.ResponseModels;
using DataLayer.DBContext;
using DataLayer.Entities;
using DataLayer.Repository;
using DataLayer.Repository.Implement;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BusinessLayer.Service.Implement
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<OrderDetailResponseModel>> AddOrderDetailAsync(CreateOrderDetailRequestModel model)
        {
            try
            {
                var orderDetail = _mapper.Map<OrderDetail>(model);
                orderDetail.Status = true;
                await _orderDetailRepository.AddOrderDetailAsync(orderDetail);
                return new BaseResponse<OrderDetailResponseModel>
                {
                    Code = 201,
                    Success = true,
                    Message = "Add new Order Detail success!.",
                    Data = _mapper.Map<OrderDetailResponseModel>(orderDetail)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderDetailResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<OrderDetailResponseModel>> DeleteOrderDetailASync(int orderDetailId, int status)
        {
            try
            {
                var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderDetailId);
                if (orderDetail == null)
                {
                    return new BaseResponse<OrderDetailResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Order Detail!.",
                        Data = null
                    };
                }
                orderDetail.Status = true;
                await _orderDetailRepository.UpdateOrderDetailAsync(orderDetail);
                return new BaseResponse<OrderDetailResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<OrderDetailResponseModel>(orderDetail)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderDetailResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<OrderDetailResponseModel>> GetAllOrderDetails(GetAllOrderDetailRequestModel model)
        {
            try
            {
                var listOrderDetail = await _orderDetailRepository.GetAllOrderDetails();

                if (model.Status != null)
                {
                    listOrderDetail = listOrderDetail.Where(o => o.Status == model.Status).ToList();
                }
            
                var result = _mapper.Map<List<OrderDetailResponseModel>>(listOrderDetail);

                //If don't have error => Pagination
                var pageOrderDetail = result.OrderBy(o => o.Id).ToPagedList(model.pageNum, model.pageSize);

                return new DynamicResponse<OrderDetailResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<OrderDetailResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageOrderDetail.PageNumber,
                            Size = pageOrderDetail.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageOrderDetail.Count,
                            TotalItem = pageOrderDetail.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            role = null,
                            status = model.Status,
                            is_Verify = null,
                            is_Delete = null,
                        },
                        PageData = pageOrderDetail.ToList(),
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<OrderDetailResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error1",
                    Data = null,
                };
            }
        }

        public async Task<BaseResponse<OrderDetailResponseModel>> GetOrderDetailById(int orderDetailId)
        {
            try
            {
                var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderDetailId);
                if (orderDetail == null)
                {
                    return new BaseResponse<OrderDetailResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Order Detail!.",
                        Data = null
                    };
                }
                return new BaseResponse<OrderDetailResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<OrderDetailResponseModel>(orderDetail)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderDetailResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<OrderDetailResponseModel>> UpdateOrderDetailAsync(CreateOrderDetailRequestModel model, int id)
        {
            try
            {
                var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(id);
                if (orderDetail == null)
                {
                    return new BaseResponse<OrderDetailResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Order Detail!.",
                        Data = null,
                    };
                }
                await _orderDetailRepository.UpdateOrderDetailAsync(_mapper.Map(model, orderDetail));
                return new BaseResponse<OrderDetailResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Update Order Detail Success!",
                    Data = _mapper.Map<OrderDetailResponseModel>(orderDetail)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderDetailResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null,
                };
            }
        }
    }
}
