using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public double TotalPrice { get; set; }
        public double ShipPrice { get; set; }
        public double Deposit { get; set; }
        public DateTime Date { get; set; }
        public bool RefundStatus { get; set; }
        public int Status { get; set; }
    }
}
