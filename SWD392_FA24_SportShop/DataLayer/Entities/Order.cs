using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public double TotalPrice { get; set; }
        public double ShipPrice { get; set; }
        public double Deposit { get; set; }
        public DateTime Date { get; set; }
        public bool RefundStatus { get; set; }
        public int Status { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
