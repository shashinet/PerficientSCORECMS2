namespace Perficient.Web.Features.Authentication.Models
{
    public class PingAuthOptions
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Authority { get; set; }

        public string CallbackPath { get; set; }
    }
}
