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
using BusinessLayer.RequestModel.OrderDetail;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using BusinessLayer.ResponseModel.OrderDetail;
using BusinessLayer.ResponseModel.Payment;


namespace BusinessLayer.Service.Implement
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShirtRepository _shirtRepository;
        private readonly IOrderDetailRepository _orderDetailsRepository;
        private readonly IShirtSizeRepository _shirtSizeRepository;
        private readonly IUserRepositoty _userRepositoty;
        private readonly IMapper _mapper;
        public enum UserRole
        {
            User = 1,
            Staff = 2,
            Manager = 3
        }
        public OrderService(IOrderDetailRepository orderDetailsRepository, IOrderRepository orderRepository, IShirtRepository shirtRepository, IShirtSizeRepository shirtSizeRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderDetailsRepository = orderDetailsRepository;
            _shirtRepository = shirtRepository;
            _shirtSizeRepository = shirtSizeRepository;
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

                // Nếu model.Status có giá trị, thực hiện lọc theo Status, nếu không thì lấy tất cả
                if (model.Status.HasValue)
                {
                    listOrder = listOrder.Where(o => o.Status == model.Status.Value).ToList();
                }

                var result = _mapper.Map<List<OrderResponseModel>>(listOrder);

                // Thực hiện phân trang
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

        public async Task<BaseResponse<CartResponseModel>> AddToCart(CreateOrderDetailsForCartRequestModel model, int userId)
        {
            try
            {
                var shirtSize = await _shirtSizeRepository.GetShirtSizeByShirtIdAndSizeId(model.ShirtId, model.SizeId);
                var shirt = await _shirtRepository.GetShirtById(model.ShirtId);
                Guid uuid = Guid.NewGuid();
                if(userId == null)
                {
                    return new BaseResponse<CartResponseModel>(){
                        Code = 404,
                        Success = false,
                        Message = "User not found!.",
                        Data = null
                    };
                }

                var cart = await _orderRepository.GetCart(userId);

                if (cart == null)
                {

                    var order = new Order()
                    {
                        Id = uuid.ToString(),
                        UserId = userId,
                        TotalPrice = shirt.Price * model.Quantity,
                        RefundStatus = false,
                        Status = 1
                    };

                    await _orderRepository.CreateOrderAsync(order);

                    var orderDetails = new OrderDetail()
                    {
                        OrderId = uuid.ToString(),
                        ShirtSizeId = shirtSize.Id,
                        Quantity = model.Quantity,
                        Price = shirt.Price,
                        StatusRating = false,
                        Status = true

                    };
                    await _orderDetailsRepository.AddOrderDetailAsync(orderDetails);

                    var orderFull = await _orderRepository.GetOrderByIdAsync(uuid.ToString());
                    var listOrderDetails = await _orderDetailsRepository.GetAllOrderDetailsByOrderId(uuid.ToString());
                    return new BaseResponse<CartResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Add to Cart successfull!.",
                        Data = new CartResponseModel()
                        {
                            Id = order.Id,
                            UserId = orderFull.UserId,
                            TotalPrice = orderFull.TotalPrice,
                            ShipPrice = orderFull.ShipPrice,
                            Deposit = orderFull.Deposit,
                            RefundStatus = orderFull.RefundStatus,
                            Status = orderFull.Status,
                            OrderDetails = _mapper.Map<List<OrderDetailResponseModel>>(listOrderDetails)
                        }
                    };
                }
                else
                {
                    if(shirtSize == null)
                    {
                        return new BaseResponse<CartResponseModel>()
                        {
                            Code = 404,
                            Success = false,
                            Message = "Not found Shirt!.",
                            Data = null
                        };
                    }
                    var oldOrderDetails = await _orderDetailsRepository.GetOrderDetailAsync(cart.Id,shirtSize.Id);
                    if (oldOrderDetails == null)
                    {
                        var newOrderDetails = new OrderDetail()
                        {
                            OrderId = cart.Id,
                            ShirtSizeId = shirtSize.Id,
                            Quantity = model.Quantity,
                            Price = shirt.Price,
                            StatusRating = false,
                            Status = true
                        };

                        await _orderDetailsRepository.AddOrderDetailAsync(newOrderDetails);

                        cart.TotalPrice = cart.TotalPrice + (newOrderDetails.Quantity * newOrderDetails.Price);

                        await _orderRepository.UpdateOrderAsync(cart);

                        var listOrderDetails = await _orderDetailsRepository.GetAllOrderDetailsByOrderId(cart.Id);

                        return new BaseResponse<CartResponseModel>()
                        {
                            Code = 200,
                            Success = true,
                            Message = "Add to Cart successfull!.",
                            Data = new CartResponseModel()
                            {
                                Id = cart.Id,
                                UserId = cart.UserId,
                                TotalPrice = cart.TotalPrice,
                                ShipPrice = cart.ShipPrice,
                                Deposit = cart.Deposit,
                                RefundStatus = cart.RefundStatus,
                                Status = cart.Status,
                                OrderDetails = _mapper.Map<List<OrderDetailResponseModel>>(listOrderDetails)
                            }
                        };
                    }
                    else
                    {
                        oldOrderDetails.Quantity = oldOrderDetails.Quantity + model.Quantity;
                        await _orderDetailsRepository.UpdateOrderDetailAsync(oldOrderDetails);

                        cart.TotalPrice = cart.TotalPrice + (model.Quantity * shirt.Price);
                        await _orderRepository.UpdateOrderAsync(cart);

                        var orderFull = await _orderRepository.GetOrderByIdAsync(cart.Id);
                        var listOrderDetails = await _orderDetailsRepository.GetAllOrderDetailsByOrderId(cart.Id);
                        return new BaseResponse<CartResponseModel>()
                        {
                            Code = 200,
                            Success = true,
                            Message = "Add to Cart successfull!.",
                            Data = new CartResponseModel()
                            {
                                Id = cart.Id,
                                UserId= cart.UserId,
                                TotalPrice = cart.TotalPrice,
                                ShipPrice = cart.ShipPrice,
                                Deposit = cart.Deposit,
                                RefundStatus = cart.RefundStatus,
                                Status = cart.Status,
                                OrderDetails = _mapper.Map<List<OrderDetailResponseModel>>(listOrderDetails)
                            }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<CartResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<CartResponseModel>> GetCartByCurrentUser(int userId)
        {
            try
            {
                if (userId == null)
                {
                    return new BaseResponse<CartResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "User not found!.",
                        Data = null
                    };
                }

                var cart = await _orderRepository.GetCart(userId);

                if (cart == null)
                {
                    return new BaseResponse<CartResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Cart null!.",
                        Data = null
                    };
                }
                else
                {
                    var listOrderDetails = await _orderDetailsRepository.GetAllOrderDetailsByOrderId(cart.Id);
                    return new BaseResponse<CartResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = null,
                        Data = new CartResponseModel()
                        {
                            Id = cart.Id,
                            UserId = cart.UserId,
                            TotalPrice = cart.TotalPrice,
                            ShipPrice = cart.ShipPrice,
                            Deposit = cart.Deposit,
                            RefundStatus = cart.RefundStatus,
                            Status = cart.Status,
                            OrderDetails = _mapper.Map<List<OrderDetailResponseModel>>(listOrderDetails)
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<CartResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<CartResponseModel>> UpdateCart(UpdateCartRequestModel model)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(model.orderId);
                var orderdetails = await _orderDetailsRepository.GetOrderDetailAsync(model.orderId, model.shirtSizeId);
                var shirtsize = await _shirtSizeRepository.GetShirtSizeByIdAsync(model.shirtSizeId);
                var shirt = await _shirtRepository.GetShirtByIdFull(shirtsize.ShirtId);

                if (model.quantity <= 0)
                {
                    var requestDeteleItem = new DeteleItemInCartRequestModel()
                    {
                        orderId = model.orderId,
                        shirtSizeId = model.shirtSizeId,
                    };
                    if (requestDeteleItem != null)
                    {
                        var response = await DeteteItemInCart(requestDeteleItem);
                        return response;
                    }
                }

                var newPirce = model.quantity * shirt.Price;
                var oldPrice = shirt.Price * orderdetails.Quantity;

                var price = newPirce - oldPrice;

                order.TotalPrice = order.TotalPrice + price;
                await _orderRepository.UpdateOrderAsync(order);

                orderdetails.Quantity = model.quantity;
                await _orderDetailsRepository.UpdateOrderDetailAsync(orderdetails);

                var listOrderDetails = await _orderDetailsRepository.GetAllOrderDetailsByOrderId(model.orderId);

                return new BaseResponse<CartResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update Cart successfull!.",
                    Data = new CartResponseModel()
                    {
                        Id = order.Id,
                        UserId = order.UserId,
                        TotalPrice = order.TotalPrice,
                        ShipPrice = order.ShipPrice,
                        Deposit = order.Deposit,
                        RefundStatus = order.RefundStatus,
                        Status = order.Status,
                        OrderDetails = _mapper.Map<List<OrderDetailResponseModel>>(listOrderDetails)
                    }
                };


            }
            catch (Exception ex)
            {
                return new BaseResponse<CartResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<CartResponseModel>> DeteteItemInCart(DeteleItemInCartRequestModel model)
        {
            try
            {
                var orderDetails = await _orderDetailsRepository.GetOrderDetailAsync(model.orderId, model.shirtSizeId);
                if (orderDetails != null)
                {
                    await _orderDetailsRepository.DeleteOrderDetailAsync(orderDetails.Id);
                    var priceOrderdetails = orderDetails.Price * orderDetails.Quantity;
                    var order = await _orderRepository.GetOrderByIdAsync(model.orderId);
                    order.TotalPrice = order.TotalPrice - priceOrderdetails;
                    await _orderRepository.UpdateOrderAsync(order);
                    var listOrderDetails = await _orderDetailsRepository.GetAllOrderDetailsByOrderId(model.orderId);
                    return new BaseResponse<CartResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Detele Item in Cart successfull!.",
                        Data = new CartResponseModel()
                        {
                            Id = order.Id,
                            UserId = order.UserId,
                            TotalPrice = order.TotalPrice,
                            ShipPrice = order.ShipPrice,
                            Deposit = order.Deposit,
                            RefundStatus = order.RefundStatus,
                            Status = order.Status,
                            OrderDetails = _mapper.Map<List<OrderDetailResponseModel>>(listOrderDetails)
                        }
                    };
                }
                else
                {
                    return new BaseResponse<CartResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Item in Cart!.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<CartResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<OrderResponseModel>> AddOrder(int userId)
        {
            try
            {
                var check = await _userRepositoty.GetUserById(userId);
                if (check != null)
                {
                    var order = new Order
                    {
                        UserId = userId,
                        Id = Guid.NewGuid().ToString(),
                        Status = 1
                    };
                    await _orderRepository.CreateOrderAsync(order);
                    return new BaseResponse<OrderResponseModel>
                    {
                        Code = 201,
                        Success = true,
                        Message = "Add Order success!.",
                        Data = _mapper.Map<OrderResponseModel>(order)
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
