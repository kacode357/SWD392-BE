using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.PaymentService.VnPay.Response
{
    public class VnPayPaymentResponseModel
    {
        public string PaymentId { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public string OrderId { get; set; }
        public string OrderDescription { get; set; }
        public string TransactionId { get; set; }
        public string PaymentMethod { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
        public string PaymentDate { get; set; }
    }
}
