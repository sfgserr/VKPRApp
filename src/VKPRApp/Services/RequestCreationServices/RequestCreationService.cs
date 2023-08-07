using VKPRApp.Builders.ApiRequestBuilders;

namespace VKPRApp.Services.RequestCreationServices
{
    public class RequestCreationService : IRequestCreationService
    {
        const string _vkApiBaseUrl = "https://api.vk.com/method";
        const string _siteBaseUrl = $"{Constants.BaseUri}api";

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

        public string CreateCompleteTaskRequest(string apiKey, string userId, string taskType)
        {
            return _apiRequestBuilder.AddBase(_siteBaseUrl)
                                     .AddMethod("completeTask")
                                     .AddAttribute("userId", userId)
                                     .AddAttribute("accessToken", apiKey)
                                     .AddAttribute("taskType", taskType)
                                     .Build();
        }

        public string CreateGetUsersRequest(string apiKey)
        {
            return _apiRequestBuilder.AddBase(_siteBaseUrl)
                                     .AddMethod("getUsers")
                                     .AddAttribute("accessToken", apiKey)
                                     .Build();
        }

        public string CreateGetUserByIdRequest(string apiKey, string userId)
        {
            return _apiRequestBuilder.AddBase(_siteBaseUrl)
                                     .AddMethod("getUserById")
                                     .AddAttribute("accessToken", apiKey)
                                     .AddAttribute("userId", userId)
                                     .Build();
        }

        public string CreateAddUserRequest(string apiKey, string jsonUser)
        {
            return _apiRequestBuilder.AddBase(_siteBaseUrl)
                                     .AddMethod("addUser")
                                     .AddAttribute("accessToken", apiKey)
                                     .AddAttribute("jsonUser", jsonUser)
                                     .Build();
        }

        public string CreateSetBankCardToNullRequest(string apiKey, string userId)
        {
            return _apiRequestBuilder.AddBase(_siteBaseUrl)
                                     .AddMethod("setBankCardToNull")
                                     .AddAttribute("accessToken", apiKey)
                                     .AddAttribute("userId", userId)
                                     .Build();
        }

        public string CreateUpdateRequest(string apiKey, string jsonUser)
        {
            return _apiRequestBuilder.AddBase(_siteBaseUrl)
                                     .AddMethod("update")
                                     .AddAttribute("accessToken", apiKey)
                                     .AddAttribute("jsonUser", jsonUser)
                                     .Build();
        }

        public string CreateUpdateAutoRenewalRequest(string apiKey, string userId, bool value)
        {
            return _apiRequestBuilder.AddBase(_siteBaseUrl)
                                     .AddMethod("updateAutoRenewal")
                                     .AddAttribute("accessToken", apiKey)
                                     .AddAttribute("userId", userId)
                                     .AddAttribute("value", value.ToString())
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
