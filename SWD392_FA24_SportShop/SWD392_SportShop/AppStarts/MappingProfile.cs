using AutoMapper;
using BusinessLayer.RequestModel.User;
using BusinessLayer.ResponseModel.User;
using DataLayer.Entities;

namespace SWDProject_BE.AppStarts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //Users
            CreateMap<LoginRequestModel, User>().ReverseMap();
            CreateMap<LoginResponseModel, User>().ReverseMap();
            CreateMap<LoginRequestModel, LoginResponseModel>().ReverseMap();


        }
    }
}
