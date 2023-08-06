using VKPRApp.Builders.ApiRequestBuilders;

namespace VKPRApp.Services.RequestCreationServices
{
    public class RequestCreationService : IRequestCreationService
    {
        const string _vkApiBaseUrl = "https://api.vk.com/method";

        private readonly IApiRequestBuilder _apiRequestBuilder;

        public RequestCreationService(IApiRequestBuilder apiRequestBuilder)
        {
            _apiRequestBuilder = apiRequestBuilder;
        }

        public string CreateUsersGetRequest(string apiKey, string userId, string expiresIn)
        {
            return _apiRequestBuilder.AddBase(_vkApiBaseUrl)
                                     .AddMethod("users.get")
                                     .AddAttribute("access_token", apiKey)
                                     .AddAttribute("user_ids", userId)
                                     .AddAttribute("expires_in", expiresIn)
                                     .AddAttribute("v", "5.131")
                                     .Build();
        }

        public string CreateWallGetRequest(string[] ids, Shared.Models.Task task)
        {
            string methodName = GetWallMethodNameByTaskType(task.TaskType);

            return _apiRequestBuilder.AddBase(_vkApiBaseUrl)
                                     .AddMethod(methodName)
                                     .AddAttribute("access_token", task.UserApiKey)
                                     .AddAttribute("owner_id", ids[0])
                                     .AddAttribute("post_id", ids[1])
                                     .AddAttribute("v", "5.131")
                                     .Build();
        }

        private string GetWallMethodNameByTaskType(Shared.Models.TaskType taskType)
        {
            return taskType == Shared.Models.TaskType.Comment ? "wall.getComments" : "wall.getReposts";
        }
    }
}
