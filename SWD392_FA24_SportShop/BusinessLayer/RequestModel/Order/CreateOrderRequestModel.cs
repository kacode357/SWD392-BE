using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.Order
{
    public class CreateOrderRequestModel
    {
        public int UserId { get; set; }
        public double ShipPrice { get; set; }
        public double Deposit { get; set; }
        public DateTime Date { get; set; }
        public bool RefundStatus { get; set; }
        public int Status { get; set; }

        public List<OrderDetailRequestModel> OrderDetails { get; set; } = new List<OrderDetailRequestModel>();

    }

    public class OrderDetailRequestModel
    {
        public int ShirtId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public bool? StatusRating { get; set; }
        public string? Comment { get; set; }
        public int? Score { get; set; }
        public bool Status { get; set; }
    }
}
