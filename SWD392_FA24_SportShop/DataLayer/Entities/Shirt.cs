using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class Shirt
    {
        public Shirt()
        {
            Inventories = new HashSet<Inventory>();
            OrderDetails = new HashSet<OrderDetail>();
            ShirtSizes = new HashSet<ShirtSize>();
        }

        public int Id { get; set; }
        public int TypeShirtId { get; set; }
        public int PlayerId { get; set; }
        public string Name { get; set; } = null!;
        public int? Number { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? UrlImg { get; set; }
        public int Status { get; set; }

        public virtual Player Player { get; set; } = null!;
        public virtual TypeShirt TypeShirt { get; set; } = null!;
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ShirtSize> ShirtSizes { get; set; }
    }
}
