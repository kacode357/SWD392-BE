using BusinessLayer.Service.PaymentService.VnPay.Request;
using BusinessLayer.Service.PaymentService.VnPay.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(VnPayPaymentRequestModel model, HttpContext context);
        VnPayPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
