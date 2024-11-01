using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public class StaffDashboardDto
    {
        public int TotalOrders { get; set; }
        public int TotalPayments { get; set; }
        public double TotalSalesAmount { get; set; }
    }
}
