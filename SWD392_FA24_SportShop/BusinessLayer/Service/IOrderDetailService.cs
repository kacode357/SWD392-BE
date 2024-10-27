using BusinessLayer.RequestModel.OrderDetail;
using BusinessLayer.ResponseModel.OrderDetail;
using BusinessLayer.ResponseModels;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IOrderDetailService
    {
        Task<BaseResponse<OrderDetailResponseModel>> AddOrderDetailAsync(CreateOrderDetailRequestModel model);
        Task<BaseResponse<OrderDetailResponseModel>> UpdateOrderDetailAsync(CreateOrderDetailRequestModel model, int id);
        Task<BaseResponse<OrderDetailResponseModel>> DeleteOrderDetailASync(int orderDetailId, int status);
        Task<BaseResponse<OrderDetailResponseModel>> GetOrderDetailById(int orderDetailId);
        Task<DynamicResponse<OrderDetailResponseModel>> GetAllOrderDetailsByOrderId(string model);

    }
}
