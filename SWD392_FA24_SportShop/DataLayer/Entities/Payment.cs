using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public string Date { get; set; } = null!;
        public double Amount { get; set; }
        public string Method { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
