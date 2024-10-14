using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.ShirtSize
{
    public class CreateShirtSizeRequestModel
    {
        public int ShirtId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
