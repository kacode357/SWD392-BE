using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.PaymentService.VnPay.Request
{
    public class VnPayPaymentRequestModel
    {
        public string OrderId { get; set; }
        public double Amount { get; set; }
        public string OrderDescription { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
