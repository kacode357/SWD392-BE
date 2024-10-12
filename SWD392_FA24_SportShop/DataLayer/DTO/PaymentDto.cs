using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string OrderId { get; set; }
        public string Date { get; set; } = null!;
        public double Amount { get; set; }
        public string Method { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; }
    }
}
