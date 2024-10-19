using BusinessLayer.RequestModel.OrderDetail;
using BusinessLayer.Service.PaymentService.VnPay.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.Order
{
    public class CheckoutRequestModel
    {
        public List<GetAllOrderDetailRequestModel> OrderDetails { get; set; } // Danh sách chi tiết đơn hàng
        public VnPayPaymentRequestModel PaymentDetails { get; set; } // Thông tin thanh toán
    }
}
