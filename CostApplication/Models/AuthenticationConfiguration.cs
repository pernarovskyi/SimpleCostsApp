
namespace CostApplication.Models
{
    public class AuthenticationConfiguration
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationsMinutes { get; set; }           
    }
}
