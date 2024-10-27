using BusinessLayer.ResponseModel.OrderDetail;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModel.Order
{
    public class OrderResponseModel
    {
        public OrderResponseModel()
        {
            OrderDetails = new List<OrderDetailResponseModel>();
        }
        public string Id { get; set; }
        public int UserId { get; set; }
        public string UserUserName { get; set; }
        public double TotalPrice { get; set; }
        public double ShipPrice { get; set; }
        public double? Deposit { get; set; }
        public DateTime Date { get; set; }
        public bool RefundStatus { get; set; }
        public int Status { get; set; }
        public int NewStatus { get; set; }
        public List<OrderDetailResponseModel> OrderDetails { get; set; }

    }
}
