using AutoMapper;
using BusinessLayer.RequestModel.Payment;
using BusinessLayer.ResponseModel.Club;
using BusinessLayer.ResponseModel.Order;
using BusinessLayer.ResponseModel.Payment;
using BusinessLayer.ResponseModels;
using DataLayer.Entities;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BusinessLayer.Service.Implement
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        
        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<PaymentResponseModel>> CreatePaymentAsync(CreatePaymentRequestModel model)
        {
            try
            {
                var payment = new Payment
                {
                    OrderId = model.OrderId,
                    Amount = model.Amount,
                    Method = model.Method,
                    UserId = model.UserId,
                    Description = model.Description,
                    Date = model.Date,
                    Status = model.Status,
                };
                await _paymentRepository.CreatePaymentAsync(payment);
                return new BaseResponse<PaymentResponseModel>
                {
                    Code = 201,
                    Success = true,
                    Message = "Create Payment success!.",
                    Data = _mapper.Map<PaymentResponseModel>(payment)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PaymentResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }


        public async Task<BaseResponse<PaymentResponseModel>> GetPaymentById(int paymentId)
        {
            try
            {
                var payment = await _paymentRepository.GetPaymentById(paymentId);
                if(payment == null)
                {
                    return new BaseResponse<PaymentResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Payment!.",
                        Data = null
                    };
                }
                return new BaseResponse<PaymentResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<PaymentResponseModel>(payment)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PaymentResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<PaymentResponseModel>> GetPaymentsAsync(GetAllPaymentRequestModel model)
        {
            try
            {
                var payment = await _paymentRepository.GetAllPayments();
                if(!string.IsNullOrEmpty(model.keyWord))
                {
                    payment = payment.Where(p => p.OrderId.Equals(model.keyWord)).ToList();
                }
                if(model.Status != null)
                {
                    payment = payment.Where(p => p.Status == model.Status).ToList();
                }
                var result = _mapper.Map<List<PaymentResponseModel>>(payment);
                var pagePayment = result
                    .OrderBy(p => p.Id) // Sắp xếp theo Id tăng dần
                    .ToPagedList(model.pageNum, model.pageSize); // Phân trang với X.PagedList
                return new DynamicResponse<PaymentResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<PaymentResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pagePayment.PageNumber,
                            Size = pagePayment.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pagePayment.PageCount,
                            TotalItem = pagePayment.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            status = model.Status,
                            is_Verify = null,
                            is_Delete = null
                        },
                        PageData = pagePayment.ToList()
                    },
                };
            
            }
            catch (Exception ex)
            {
                return new DynamicResponse<PaymentResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<PaymentResponseModel>> GetPaymentByUserId(int userId)
        {
            try
            {
                var payment = await _paymentRepository.GetAllPayments();
                    payment = payment.Where(p => p.UserId.Equals(userId)).ToList();
                var result = _mapper.Map<List<PaymentResponseModel>>(payment);
                return new DynamicResponse<PaymentResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = new MegaData<PaymentResponseModel>()
                    {
                        PageInfo = null,
                        SearchInfo = null,
                        PageData = result
                    },
                };

            }
            catch (Exception ex)
            {
                return new DynamicResponse<PaymentResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<PaymentResponseModel>> UpdatePaymentAsync(CreatePaymentRequestModel model, int id)
        {
            try
            {
                var payment = await _paymentRepository.GetPaymentById(id);
                if (payment == null)
                {
                    return new BaseResponse<PaymentResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Payment!.",
                        Data = null
                    };
                }
                await _paymentRepository.UpdatePaymentAsync(_mapper.Map(model, payment));
                return new BaseResponse<PaymentResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update Club Success!.",
                    Data = _mapper.Map<PaymentResponseModel>(payment)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PaymentResponseModel>()
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
