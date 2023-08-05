
namespace VKPRApp.Shared.Models
{
    public enum TaskType
    {
        Comment,
        Repost
    }

    public class Task : DomainObject
    {
        public Task(string postUrl, int reward, int count, string userId, string userApiKey, int price)
        {
            PostUrl = postUrl;
            Reward = reward;
            Count = count;
            UserId = userId;
            UserApiKey = userApiKey;
            Price = price;
        }

        public TaskType TaskType { get; set; }
        public string PostUrl { get; set; } = string.Empty;
        public int Reward { get; set; } = 0;
        public int Count { get; set; } = 0;
        public string UserId { get; set; } = string.Empty;
        public string UserApiKey { get; set; } = string.Empty;
        public int Price { get; set; } = 0;
    }
}
