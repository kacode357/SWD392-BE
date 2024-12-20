﻿using BusinessLayer.RequestModel.Order;
using BusinessLayer.RequestModel.OrderDetail;
using BusinessLayer.ResponseModel.Order;
using BusinessLayer.ResponseModels;
using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IOrderService
    {
        //Task<BaseResponse<OrderResponseModel>> CreateOrderAsync(CreateOrderRequestModel model);
        Task<BaseResponse<OrderResponseModel>> UpdateOrderAsync(CreateOrderRequestModel model, string id);
        Task<BaseResponse<OrderResponseModel>> DeleteOrderAsync(string orderId, int status);
        Task<BaseResponse<OrderResponseModel>> GetOrderById(string orderId);
        Task<DynamicResponse<OrderResponseModel>> GetAllOrders(SearchOrderByIdRequestModel model);
        //Task<BaseResponse<OrderResponseModel>> CalculatePrice (int orderId);
        //Task<BaseResponse<OrderResponseModel>> GetOrdersByUserIdAsync (int userId);
        Task<DynamicResponse<OrderResponseModel>> GetOrdersByCurrentUser (GetOrderByCurrentUserRequestModel model, int userId);
        //Task<BaseResponse<OrderResponseModel>> GetOrderDetailsByOrderIdAsync (int orderId);
        Task<BaseResponse<OrderResponseModel>> ChangeOrderStatusAsync (string orderId, string jwtToken, int newStatus);
        Task<BaseResponse<OrderResponseModel>> ProcessRefundAsync (string orderId);
        Task<BaseResponse<CartResponseModel>> AddToCart(CreateOrderDetailsForCartRequestModel model, int userId);
        Task<BaseResponse<CartResponseModel>> GetCartByCurrentUser(int userId);
        Task<BaseResponse<CartResponseModel>> UpdateCart(UpdateCartRequestModel model);
        Task<BaseResponse<CartResponseModel>> DeteteItemInCart(DeteleItemInCartRequestModel model);
        Task<BaseResponse<OrderResponseModel>> AddOrder(int userId);
        //Task<BaseResponse<OrderResponseModel>> GetAllOrdersByStatus(GetAllOrderRequestModel model);
        Task<BaseResponse<OrderResponseModel>> AddReviewAsync(AddReviewRequestModel model);
        Task<BaseResponse<OrderResponseModel>> EditReviewAsync(int orderDetailId, int scoreRating, string comment);
        Task<BaseResponse<OrderResponseModel>> DeleteReviewAsync(int orderDetailId);
        Task<BaseResponse<ReviewResponseModel>> GetReviewByOrderDetailIdAsync(int orderDetailId);
        Task<BaseResponse<List<ReviewResponseModel>>> GetReviewsByShirtIdAsync (int shirtId);
        Task<BaseResponse> SendRejectOrderEmail(string email, string orderId);
    }
}
