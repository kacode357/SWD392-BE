using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.Payment
{
    public class GetAllPaymentRequestModel
    {
        public int pageNum { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public int? UserId { get; set; }
        public int? OrderId { get; set; }
        public int? Status { get; set; }
    }
}
