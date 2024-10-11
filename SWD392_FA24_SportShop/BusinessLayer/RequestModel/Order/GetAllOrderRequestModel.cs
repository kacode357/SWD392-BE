using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.Order
{
    public class GetAllOrderRequestModel
    {
        public int pageNum { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public int? Status { get; set; }
        public DateTime? Date {  get; set; }
    }
}
