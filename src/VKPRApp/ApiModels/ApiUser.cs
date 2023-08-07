using Newtonsoft.Json;

namespace VKPRApp.ApiModels
{
    public class ApiUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("can_access_closed")]
        public bool CanAccessClosed { get; set; }
        [JsonProperty("is_closed")]
        public bool IsClosed { get; set; }
    }
}
