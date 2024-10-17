using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public bool StatusRating { get; set; }
        public string? Comment { get; set; }
        public int? Score { get; set; }
        public bool Status { get; set; }
        public int ShirtSizeId { get; set; }

        public virtual Order Order { get; set; }
        public virtual ShirtSize ShirtSize { get; set; }
    }
}
