using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.Order
{
    public class DeteleItemInCartRequestModel
    {
        public string orderId { get; set; }
        public int shirtSizeId { get; set; }
    }
}
