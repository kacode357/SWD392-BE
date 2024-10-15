using AutoMapper;
using BusinessLayer.RequestModel.Club;
using BusinessLayer.RequestModel.Order;
using BusinessLayer.RequestModel.OrderDetail;
using BusinessLayer.RequestModel.Payment;
using BusinessLayer.RequestModel.Player;
using BusinessLayer.RequestModel.Session;
using BusinessLayer.RequestModel.Shirt;
using BusinessLayer.RequestModel.ShirtSize;
using BusinessLayer.RequestModel.Size;
using BusinessLayer.RequestModel.TypeShirt;
using BusinessLayer.RequestModel.User;
using BusinessLayer.ResponseModel.Club;
using BusinessLayer.ResponseModel.Order;
using BusinessLayer.ResponseModel.OrderDetail;
using BusinessLayer.ResponseModel.Payment;
using BusinessLayer.ResponseModel.Player;
using BusinessLayer.ResponseModel.Session;
using BusinessLayer.ResponseModel.Shirt;
using BusinessLayer.ResponseModel.ShirtSize;
using BusinessLayer.ResponseModel.Size;
using BusinessLayer.ResponseModel.TypeShirt;
using BusinessLayer.ResponseModel.User;
using DataLayer.DTO;
using DataLayer.Entities;

namespace SWDProject_BE.AppStarts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //Users
            CreateMap<RegisterRequestModel, User>().ReverseMap();
            CreateMap<UserResponseModel, User>().ReverseMap();
            CreateMap<RegisterRequestModel, UserResponseModel>().ReverseMap();
            CreateMap<UpdateRequestModel, User>().ReverseMap();

            //Club
            CreateMap<CreateClubRequestModel, Club>().ReverseMap();
            CreateMap<CreateClubRequestModel, ClubResponseModel>().ReverseMap();
            CreateMap<CreateClubRequestModel, Club>().ReverseMap();
            CreateMap<ClubResponseModel, Club>().ReverseMap();

            //Session
            CreateMap<CreateSessionRequestModel, Session>().ReverseMap();
            CreateMap<CreateSessionRequestModel, SessionResponseModel>().ReverseMap();
            CreateMap<CreateSessionRequestModel, Session>().ReverseMap();
            CreateMap<SessionResponseModel, Session>().ReverseMap();

            //Shirt
            CreateMap<CreateShirtRequestModel, Shirt>().ReverseMap();
            CreateMap<CreateShirtRequestModel, ShirtResponseModel>().ReverseMap();
            CreateMap<CreateShirtRequestModel, Shirt>().ReverseMap();
            CreateMap<ShirtResponseModel, Shirt>().ReverseMap();
            CreateMap<ShirtDto, ShirtResponseModel>();

            //Player
            CreateMap<CreatePlayerRequestModel, Player>().ReverseMap();
            CreateMap<CreatePlayerRequestModel, PlayerResponseModel>().ReverseMap();
            CreateMap<CreatePlayerRequestModel, Player>().ReverseMap();
            CreateMap<PlayerResponseModel, Player>().ReverseMap();
            CreateMap<PlayerDto, PlayerResponseModel>();

            //TypeShirt
            CreateMap<CreateTypeShirtRequestModel, TypeShirt>().ReverseMap();
            CreateMap<CreateTypeShirtRequestModel, TypeShirtResponseModel>().ReverseMap();
            CreateMap<CreateTypeShirtRequestModel, TypeShirt>().ReverseMap();
            CreateMap<TypeShirtResponseModel, TypeShirt>().ReverseMap();
            CreateMap<TypeShirtDto, TypeShirtResponseModel>();

            //Order
            CreateMap<CreateOrderRequestModel, Order>().ReverseMap();
            CreateMap<CreateOrderRequestModel, OrderResponseModel>().ReverseMap();
            CreateMap<CreateOrderRequestModel, Order>().ReverseMap();
            CreateMap<OrderResponseModel, Order>().ReverseMap();
            CreateMap<OrderDto, OrderResponseModel>().ReverseMap();
            CreateMap<CartResponseModel, Order>().ReverseMap();

            //OrderDetail
            CreateMap<CreateOrderDetailRequestModel, OrderDetail>().ReverseMap();
            CreateMap<CreateOrderDetailRequestModel, OrderDetailResponseModel>().ReverseMap();
            CreateMap<CreateOrderRequestModel, OrderDetail>().ReverseMap();
            CreateMap<OrderDetailResponseModel, OrderDetail>().ReverseMap();
            CreateMap<OrderDetailDto, OrderDetailResponseModel>();

            //Payment
            CreateMap<CreatePaymentRequestModel, Payment>().ReverseMap();
            CreateMap<CreatePaymentRequestModel, PaymentResponseModel>().ReverseMap();
            CreateMap<CreatePaymentRequestModel, Payment>().ReverseMap();
            CreateMap<PaymentResponseModel, Payment>().ReverseMap();
            CreateMap<PaymentDto, PaymentResponseModel>();

            //Size
            CreateMap<CreateSizeRequestModel, Size>().ReverseMap();
            CreateMap<CreateSizeRequestModel, SizeResponseModel>().ReverseMap();
            CreateMap<CreateSizeRequestModel, Size>().ReverseMap();
            CreateMap<SizeResponseModel, Size>().ReverseMap();

            //ShirtSize
            CreateMap<CreateShirtSizeRequestModel, ShirtSize>().ReverseMap();
            CreateMap<CreateShirtSizeRequestModel, ShirtSizeResponseModel>().ReverseMap();
            CreateMap<CreateShirtSizeRequestModel, ShirtSize>().ReverseMap();
            CreateMap<ShirtResponseModel, ShirtSize>().ReverseMap();
        }
    }
}
