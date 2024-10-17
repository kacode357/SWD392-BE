using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class ShirtSize
    {
        public ShirtSize()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int ShirtId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public virtual Shirt Shirt { get; set; }
        public virtual Size Size { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
