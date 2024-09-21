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
            CreateMap<RegisterRequestModel, User>().ReverseMap();
            CreateMap<RegisterResponseModel, User>().ReverseMap();
            CreateMap<RegisterRequestModel, RegisterResponseModel>().ReverseMap();
            CreateMap<UpdateRequestModel, User>().ReverseMap();


        }
    }
}
