using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModel.OrderDetail
{
    public class OrderDetailResponseModel
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public int ShirtSizeId { get; set; }
        public int ShirtId { get; set; }
        public string ShirtName { get; set; }
        public string ShirtUrlImg { get; set; }
        public double ShirtPrice { get; set; }
        public string ShirtDescription { get; set; }
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public string SizeDescription { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public bool StatusRating { get; set; }
        public string? Comment { get; set; }
        public int? Score { get; set; }
        public bool Status { get; set; }
    }
}
