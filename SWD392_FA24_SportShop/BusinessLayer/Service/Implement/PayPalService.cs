using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Implement
{
    public class PayPalService
    {
        private readonly IConfiguration _configuration;
        public PayPalService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public PayPalEnvironment GetEnvironment()
        {
            var clientId = _configuration["PayPal:ClientId"];
            var clientSecret = _configuration["PayPal:ClientSecret"];
            var environment = _configuration["PayPal:Environment"];

            if (environment.Equals("live"))
            {
                return new PayPalEnvironment.Live(clientId, clientSecret);
            }
            return new PayPalEnvironment.Sandbox(clientId, clientSecret);
        }

        public PayPalHttpClient GetHttpClient()
        {
            var environment = GetEnvironment();
            return new PayPalHttpClient(environment);
        }
    }
}
}
