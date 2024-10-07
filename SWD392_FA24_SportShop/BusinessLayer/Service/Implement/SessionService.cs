using AutoMapper;
using BusinessLayer.RequestModel.Session;
using BusinessLayer.ResponseModel.Club;
using BusinessLayer.ResponseModel.Session;
using BusinessLayer.ResponseModels;
using DataLayer.Entities;
using DataLayer.Repository;
using DataLayer.Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BusinessLayer.Service.Implement
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMapper _mapper;
        public SessionService(ISessionRepository sessionRepository, IMapper mapper)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<SessionResponseModel>> CreateSessionAsync(CreateSessionRequestModel model)
        {
            try
            {
                var session = _mapper.Map<Session>(model);
                session.Status = true;
                await _sessionRepository.CreateSessionAsync(session);
                return new BaseResponse<SessionResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Create Session success!.",
                    Data = _mapper.Map<SessionResponseModel>(session)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<SessionResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<SessionResponseModel>> DeleteSessionAsync(int sessionId, bool status)
        {
            try
            {
                var session = await _sessionRepository.GetSessionById(sessionId);
                if (session == null)
                {
                    return new BaseResponse<SessionResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Session!.",
                        Data = null
                    };
                }
                session.Status = status;
                await _sessionRepository.UpdateSessionAsync(session);
                return new BaseResponse<SessionResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<SessionResponseModel>(session)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<SessionResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }

        }

        public async Task<BaseResponse<SessionResponseModel>> GetSessionById(int sessionId)
        {
            try
            {
                var session = await _sessionRepository.GetSessionById(sessionId);
                if(session == null)
                {
                    return new BaseResponse<SessionResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Session!",
                        Data = null
                    };
                }
                return new BaseResponse<SessionResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<SessionResponseModel>(session)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<SessionResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<SessionResponseModel>> GetSessions(GetAllSessionRequestModel model)
        {
            try
            {
                var listSession = await _sessionRepository.GetSessions();

                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    List<Session> listSessionByName = listSession.Where(c => c.Name.ToLower().Contains(model.keyWord)).ToList();

                    listSession = listSessionByName
                               .GroupBy(c => c.Id)
                               .Select(g => g.First())
                               .ToList();
                }
                if (model.Status != null)
                {
                    listSession = listSession.Where(c => c.Status == model.Status).ToList();
                }
                var result = _mapper.Map<List<SessionResponseModel>>(listSession);
                // Nếu không có lỗi, thực hiện phân trang
                var pageSession = result// Giả sử result là danh sách người dùng
                    .OrderBy(c => c.Id) // Sắp xếp theo Id tăng dần
                    .ToPagedList(model.pageNum, model.pageSize); // Phân trang với X.PagedList
                return new DynamicResponse<SessionResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<SessionResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageSession.PageNumber,
                            Size = pageSession.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageSession.PageCount,
                            TotalItem = pageSession.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            status = model.Status,
                            is_Verify = null,
                            is_Delete = null
                        },
                        PageData = pageSession.ToList()
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<SessionResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }
        public async Task<BaseResponse<SessionResponseModel>> UpdateSessionAsync(CreateSessionRequestModel model, int id)
        {
            try
            {
                var session = await _sessionRepository.GetSessionById(id);
                if (session == null)
                {
                    return new BaseResponse<SessionResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Session!.",
                        Data = null
                    };
                }
                await _sessionRepository.UpdateSessionAsync(_mapper.Map(model, session));
                return new BaseResponse<SessionResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update Session Success!.",
                    Data = _mapper.Map<SessionResponseModel>(session)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<SessionResponseModel>()
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
