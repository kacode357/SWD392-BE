using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayPalCheckoutSdk.Core;

namespace BusinessLayer.Service.Implement
{
    public class PayPalService
    {
        private readonly PayPalHttpClient _client;

        public PayPalService(string clientId, string clientSecret)
        {
            var environment = new SandboxEnvironment(clientId, clientSecret); // Sử dụng LiveEnvironment cho môi trường thực
            _client = new PayPalHttpClient(environment);
        }

        public PayPalHttpClient GetHttpClient()
        {
            return _client;
        }
    }
}
