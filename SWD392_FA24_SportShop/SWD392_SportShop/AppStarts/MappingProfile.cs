using AutoMapper;
using BusinessLayer.RequestModel.Club;
using BusinessLayer.RequestModel.Player;
using BusinessLayer.RequestModel.Session;
using BusinessLayer.RequestModel.Shirt;
using BusinessLayer.RequestModel.User;
using BusinessLayer.ResponseModel.Club;
using BusinessLayer.ResponseModel.Player;
using BusinessLayer.ResponseModel.Session;
using BusinessLayer.ResponseModel.Shirt;
using BusinessLayer.ResponseModel.User;
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

            //Player
            CreateMap<CreatePlayerRequestModel, Player>().ReverseMap();
            CreateMap<CreatePlayerRequestModel, PlayerResponseModel>().ReverseMap();
            CreateMap<CreatePlayerRequestModel, Player>().ReverseMap();
            CreateMap<PlayerResponseModel, Player>().ReverseMap();

        }
    }
}
