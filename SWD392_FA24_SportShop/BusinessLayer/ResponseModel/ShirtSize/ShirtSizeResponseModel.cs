using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModel.ShirtSize
{
    public class ShirtSizeResponseModel
    {
        public int Id { get; set; }
        public int ShirtId { get; set; }
        public string ShirtName { get; set; }
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public string SizeDescription { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
