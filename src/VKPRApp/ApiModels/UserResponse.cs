using Newtonsoft.Json;

namespace VKPRApp.ApiModels
{
    public class UserResponse
    {
        [JsonProperty("response")]
        public List<ApiUser> Response { get; set; }
    }
}
