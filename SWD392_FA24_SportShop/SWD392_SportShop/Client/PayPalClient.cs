namespace SWD392_SportShop.Client
{
    public class PayPalClient
    {
        public int MyProperty { get; set; }
        public string Environment { get; }
        public string ClientId { get; }
        public string ClientSecret { get; }
        public string BaseUrl => Environment == "Live"
            ? "https://api-m.paypal.com"
            : "https://api-m.sandbox.paypal.com";

        public PayPalClient(string clientId, string clientSecret, string environment)
        {
            Environment = environment;
            ClientId = clientId;
            ClientSecret = clientSecret;
        }
    }
}
