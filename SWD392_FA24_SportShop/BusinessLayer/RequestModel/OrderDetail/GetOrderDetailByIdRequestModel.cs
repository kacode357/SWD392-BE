using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.OrderDetail
{
    public class GetOrderDetailByIdRequestModel
    {
        public string OrderId { get; set; }
        public int ShirtId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public bool StatusRating { get; set; }
        public string? Comment { get; set; }
        public int? Score { get; set; }
        public bool Status { get; set; }
    }
}
