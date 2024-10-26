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
using BusinessLayer.Service.PaymentService.VnPay.Request;
using BusinessLayer.Service.PaymentService.VnPay.Response;
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
            CreateMap<Shirt, ShirtResponseModel>()
                .ForMember(dest => dest.SessionId, opt => opt.MapFrom(src => src.TypeShirt.Session.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Player.FullName))
                .ForMember(dest => dest.SessionName, opt => opt.MapFrom(src => src.TypeShirt.Session.Name))
                .ForMember(dest => dest.ClubId, opt => opt.MapFrom(src => src.Player.Club.Id))
                .ForMember(dest => dest.ClubName, opt => opt.MapFrom(src => src.Player.Club.Name))
                .ForMember(dest => dest.ClubEstablishedYear, opt => opt.MapFrom(src => src.Player.Club.EstablishedYear))
                .ForMember(dest => dest.ClubLogo, opt => opt.MapFrom(src => src.Player.Club.ClubLogo))
                .ForMember(dest => dest.ClubCountry, opt => opt.MapFrom(src => src.Player.Club.Country))
                .ForMember(dest => dest.ListSize, opt => opt.MapFrom(src => src.ShirtSizes));
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
            CreateMap<OrderDetail, OrderDetailResponseModel>()
                .ForMember(dest => dest.ShirtId, opt => opt.MapFrom(src => src.ShirtSize.Shirt.Id))
                .ForMember(dest => dest.ShirtDescription, opt => opt.MapFrom(src => src.ShirtSize.Shirt.Description))
                .ForMember(dest => dest.ShirtPrice, opt => opt.MapFrom(src => src.ShirtSize.Shirt.Price))
                .ForMember(dest => dest.ShirtName, opt => opt.MapFrom(src => src.ShirtSize.Shirt.Name))
                .ForMember(dest => dest.ShirtUrlImg, opt => opt.MapFrom(src => src.ShirtSize.Shirt.UrlImg))
                .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.ShirtSize.Size.Id))
                .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.ShirtSize.Size.Name))
                .ForMember(dest => dest.SizeDescription, opt => opt.MapFrom(src => src.ShirtSize.Size.Description));
            CreateMap<OrderDetailDto, OrderDetailResponseModel>().ReverseMap();

            //Payment
            CreateMap<VnPayPaymentRequestModel, Payment>().ReverseMap();
            CreateMap<VnPayPaymentRequestModel, VnPayPaymentResponseModel>().ReverseMap();
            CreateMap<VnPayPaymentRequestModel, Payment>().ReverseMap();
            CreateMap<VnPayPaymentResponseModel, Payment>().ReverseMap();
            CreateMap<Payment, VnPayPaymentResponseModel>();
            CreateMap<PaymentResponseModel, Payment>().ReverseMap();

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
            CreateMap<ShirtSizeResponseModel, ShirtSize>().ReverseMap();
        }
    }
}
