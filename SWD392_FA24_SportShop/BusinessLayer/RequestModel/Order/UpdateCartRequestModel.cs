using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.Order
{
    public class UpdateCartRequestModel
    {
        public string orderId { get; set; }
        public int shirtSizeId { get; set; }
        public int quantity { get; set; }

    }
}
