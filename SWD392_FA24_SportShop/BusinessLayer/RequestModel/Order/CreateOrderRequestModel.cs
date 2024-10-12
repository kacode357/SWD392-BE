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
        public double? TotalPrice { get; set; }
        public double ShipPrice { get; set; }
        public double Deposit { get; set; }
        public DateTime Date { get; set; }
        public bool RefundStatus { get; set; }
        public int Status { get; set; }

    }

}
