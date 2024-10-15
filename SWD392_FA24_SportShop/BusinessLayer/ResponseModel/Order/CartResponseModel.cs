using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModel.Order
{
    public class CartResponseModel
    {
        public CartResponseModel()
        {
            OrderDetails = new HashSet<DataLayer.Entities.OrderDetail>();
        }

        public int UserId { get; set; }
        public double? TotalPrice { get; set; }
        public double? ShipPrice { get; set; }
        public double? Deposit { get; set; }
        public DateTime? Date { get; set; }
        public bool RefundStatus { get; set; }
        public int Status { get; set; }
        public string Id { get; set; }

        public virtual ICollection<DataLayer.Entities.OrderDetail> OrderDetails { get; set; }
    }
}
