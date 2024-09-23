using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public int ShirtId { get; set; }
        public double Quantity { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        public virtual Shirt Shirt { get; set; }
    }
}
