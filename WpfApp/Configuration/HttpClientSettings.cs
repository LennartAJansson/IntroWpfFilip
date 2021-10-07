
namespace WpfApp.Configuration
{
    public class HttpClientSettings
    {
        public const string SectionName = "HttpClientSettings";
        public string Url { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}