using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.OrderDetail
{
    public class CreateOrderDetailsForCartRequestModel
    {
        public int ShirtId { get; set; }
        public int Quantity { get; set; }
    }
}
