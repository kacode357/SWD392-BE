using AutoMapper;
using BusinessLayer.RequestModel.Order;
using BusinessLayer.ResponseModel.Order;
using BusinessLayer.ResponseModels;
using DataLayer.Entities;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using System.IdentityModel.Tokens.Jwt;

namespace BusinessLayer.Service.Implement
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public enum UserRole
        {
            User = 1,
            Staff = 2,
            Manager = 3
        }
        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        private UserRole GetCurrentUserRole(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtTokenObj = handler.ReadJwtToken(jwtToken);

            var roleClaim = jwtTokenObj.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

            return Enum.TryParse(roleClaim, out UserRole role) ? role : UserRole.User; // Default is User if do not found role
        }

        /*        public async Task<BaseResponse<OrderResponseModel>> CalculatePrice(int orderId)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(orderId);
                if (order == null)
                {
                    return new BaseResponse<OrderResponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Order not found.",
                        Data = null
                    };
                }
                var totalPrice = order.OrderDetails.Sum(d => d.Quantity * d.Price);

                if (order.ShipPrice != null)
                {
                    totalPrice += order.ShipPrice;
                }

                if (order.Deposit != null)
                {
                    totalPrice -= order.Deposit;
                }

                order.TotalPrice = totalPrice;

                await _orderRepository.UpdateOrderAsync(order);

                return new BaseResponse<OrderResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Price calculated successfully.",
                    Data = _mapper.Map<OrderResponseModel>(order)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }
*/        
        public async Task<BaseResponse<OrderResponseModel>> ChangeOrderStatusAsync(string orderId, string jwtToken, int newStatus)
        {
            try
            {
                var currentUserRole = GetCurrentUserRole(jwtToken);
                var order = await _orderRepository.GetOrderByIdAsync(orderId);

                if (order == null)
                {
                    return new BaseResponse<OrderResponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Order not found!.",
                        Data = null
                    };
                }

                switch (currentUserRole)
                {
                    case UserRole.User:
                        if (newStatus == 6) //6: Cancelled
                        {
                            if (order.Status == 1) //1: Pending
                            {
                                order.Status = newStatus;
                                await _orderRepository.UpdateOrderAsync(order);
                                return new BaseResponse<OrderResponseModel>
                                {
                                    Code = 200,
                                    Success = true,
                                    Message = "Order cancelled successfully!",
                                    Data = _mapper.Map<OrderResponseModel>(order)
                                };
                            }
                            return new BaseResponse<OrderResponseModel>
                            {
                                Code = 400,
                                Success = false,
                                Message = "Only orders with Pending status can be cancelled!",
                                Data = null
                            };
                        }
                        break;

                    case UserRole.Staff:
                        if (order.Status == 1 && newStatus == 2) //Pending -> Confirmed
                        {
                            order.Status = newStatus;
                            await _orderRepository.UpdateOrderAsync(order);
                            return new BaseResponse<OrderResponseModel>
                            {
                                Code = 200,
                                Success = true,
                                Message = "Order confirmed successfully!",
                                Data = _mapper.Map<OrderResponseModel>(order)
                            };
                        }
                        else if (order.Status == 2 &&  newStatus == 3) //Confirmed -> Processing
                        {
                            order.Status = newStatus;
                            await _orderRepository.UpdateOrderAsync(order);
                            return new BaseResponse<OrderResponseModel>
                            {
                                Code = 200,
                                Success = true,
                                Message = "Order is now processing!",
                                Data = _mapper.Map<OrderResponseModel>(order)
                            };
                        }
                        else if (order.Status == 3 && newStatus == 4) // Processing -> Shipped
                        {
                            order.Status = newStatus;
                            await _orderRepository.UpdateOrderAsync(order);
                            return new BaseResponse<OrderResponseModel>
                            {
                                Code = 200,
                                Success = true,
                                Message = "Order has been shipped.",
                                Data = _mapper.Map<OrderResponseModel>(order)
                            };
                        }
                        break;

                    case UserRole.Manager:
                        if (newStatus == 2 && order.Status == 1) // Pending -> Confirmed
                        {
                            order.Status = newStatus;
                            await _orderRepository.UpdateOrderAsync(order);
                            return new BaseResponse<OrderResponseModel>
                            {
                                Code = 200,
                                Success = true,
                                Message = "Order confirmed by manager.",
                                Data = _mapper.Map<OrderResponseModel>(order)
                            };
                        }
                        else if (newStatus == 7 && order.Status == 4) // Delivered -> Refunded
                        {
                            order.Status = newStatus;
                            await _orderRepository.UpdateOrderAsync(order);
                            return new BaseResponse<OrderResponseModel>
                            {
                                Code = 200,
                                Success = true,
                                Message = "Order has been refunded.",
                                Data = _mapper.Map<OrderResponseModel>(order)
                            };
                        }
                        break;

                    default:
                        return new BaseResponse<OrderResponseModel>
                        {
                            Code = 403,
                            Success = false,
                            Message = "You do not have permission to change the order status.",
                            Data = null
                        };
                }

                return new BaseResponse<OrderResponseModel>
                {
                    Code = 400,
                    Success = false,
                    Message = "Invalid status change.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<OrderResponseModel>> CreateOrderAsync(CreateOrderRequestModel model)
        {
            try
            {
                if (model == null || model.UserId <= 0)
                {
                    return new BaseResponse<OrderResponseModel>
                    {
                        Code = 400,
                        Success = false,
                        Message = "Invalid order data. Order details are required.",
                        Data = null
                    };
                }

                var order = new Order
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = model.UserId,
                    ShipPrice = model.ShipPrice,
                    Deposit = model.Deposit,
                    Date = model.Date,
                    Status = 1, //1: Pending
                    TotalPrice = 0
                };

                double totalPrice = 0;

                if (order.Deposit > 0)
                {
                    // Không cho phép trừ nhiều hơn tổng giá trị hiện có (bao gồm phí ship)
                    if (totalPrice - order.Deposit >= 0)
                    {
                        totalPrice -= order.Deposit;
                    }
                    else
                    {
                        totalPrice = 0; // Nếu đặt cọc lớn hơn tổng hiện có, thì đặt tổng là 0
                    }
                }

                if (totalPrice < 0)
                {
                    return new BaseResponse<OrderResponseModel>
                    {
                        Code = 400,
                        Success = false,
                        Message = "Total price cannot be less than zero.",
                        Data = null
                    };
                }

                order.TotalPrice = totalPrice;
                order.Status = 1; //Default is 1: Pending

                await _orderRepository.CreateOrderAsync(order);

                return new BaseResponse<OrderResponseModel>
                {
                    Code = 201,
                    Success = true,
                    Message = "Order created successfully!",
                    Data = _mapper.Map<OrderResponseModel>(order)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<OrderResponseModel>> DeleteOrderAsync(string orderId, int status)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(orderId);
                if (order == null)
                {
                    return new BaseResponse<OrderResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Order!.",
                        Data = null
                    };
                }
                order.Status = 6;
                await _orderRepository.UpdateOrderAsync(order);
                return new BaseResponse<OrderResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<OrderResponseModel>(order)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<OrderResponseModel>> GetAllOrders(GetAllOrderRequestModel model)
        {
            try
            {
                var listOrder = await _orderRepository.GetAllOrders();
                /*//Filtering by status
                if (model.Status != null)
                {
                    listOrder = listOrder.Where(o => o.Status == model.Status).ToList();
                }
                //Filtering by User Id
                if (model.UserId != null)
                {
                    listOrder = listOrder.Where(o => o.UserId == model.UserId).ToList();
                }*/
                var result = _mapper.Map<List<OrderResponseModel>>(listOrder);

                //If don't have error => Pagination
                var pageOrder = result.OrderBy(o => o.Id).ToPagedList(model.pageNum, model.pageSize);

                return new DynamicResponse<OrderResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<OrderResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageOrder.PageNumber,
                            Size = pageOrder.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageOrder.PageCount,
                            TotalItem = pageOrder.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            role = null,
                            status = model.Status.HasValue ? (model.Status.Value == 1) : (bool?)null,
                            is_Verify = null,
                            is_Delete = null,
                        },
                        PageData = pageOrder.ToList()
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<OrderResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<OrderResponseModel>> GetOrderById(string orderId)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(orderId);
                if (order == null)
                {
                    return new BaseResponse<OrderResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Order!.",
                        Data = null,
                    };
                }
                return new BaseResponse<OrderResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<OrderResponseModel>(order)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        /*public async Task<BaseResponse<OrderResponseModel>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            try
            {
                var order = await _orderRepository.GetOrderDetailsByOrderIdAsync(orderId);

                if (order == null)
                {
                    return new BaseResponse<OrderResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Order not found!",
                        Data = null
                    };
                }

                return new BaseResponse<OrderResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<OrderResponseModel>(order)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<OrderResponseModel>> GetOrdersByUserIdAsync(int userId)
        {
            try
            {
                var order = await _orderRepository.GetOrdersByUserIdAsync(userId);
                if (order == null || !order.Any())
                {
                    return new BaseResponse<OrderResponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "No orders found for the user.",
                        Data = null
                    };
                }

                return new BaseResponse<OrderResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Orders retrieved successfully.",
                    Data = _mapper.Map<OrderResponseModel>(order)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }*/

        public async Task<BaseResponse<OrderResponseModel>> ProcessRefundAsync(string orderId)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(orderId);

                if (order == null)
                {
                    return new BaseResponse<OrderResponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Order not found.",
                        Data = null
                    };
                }
                
                if (order.Status != 5) //5: Delivered
                {
                    return new BaseResponse<OrderResponseModel>
                    {
                        Code = 400,
                        Success = false,
                        Message = "Refund can only be processed for delivered orders.",
                        Data = null
                    };
                }

                order.Status = 7; //7: Refunded
                await _orderRepository.UpdateOrderAsync(order);

                return new BaseResponse<OrderResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Refund processed sucecessfully!",
                    Data = _mapper.Map<OrderResponseModel>(order)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
            
        }

        public async Task<BaseResponse<OrderResponseModel>> UpdateOrderAsync(CreateOrderRequestModel model, string id)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(id);
                if (order == null)
                {
                    return new BaseResponse<OrderResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Order!.",
                        Data = null,
                    };
                }
                await _orderRepository.UpdateOrderAsync(_mapper.Map(model, order));
                return new BaseResponse<OrderResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update Order Success!",
                    Data = _mapper.Map<OrderResponseModel>(order)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderResponseModel>()
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
