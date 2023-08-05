
namespace VKPRApp.Shared.Models
{
    public class VKUser : DomainObject
    {
        public VKUser(string apiKey, string userId)
        {
            ApiKey = apiKey;
            UserId = userId;
        }

        public string ApiKey { get; set; }
        public string UserId { get; set; }
    }
}
