using AutoMapper;
using BusinessLayer.RequestModel.Club;
using BusinessLayer.RequestModel.User;
using BusinessLayer.ResponseModel.Club;
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

        }
    }
}
