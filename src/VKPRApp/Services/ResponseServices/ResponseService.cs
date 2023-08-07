using Newtonsoft.Json;
using VKPRApp.ApiModels;
using VKPRApp.Services.RequestCreationServices;
using VKPRApp.Shared.Services;

namespace VKPRApp.Services.ResponseServices
{
    public class ResponseService : IResponseService
    {
        private readonly HttpClient _client;
        private readonly IUserService _userService;
        private readonly IRequestCreationService _requestCreationService;

        public ResponseService(HttpClient client, IUserService userService, IRequestCreationService requestCreationService)
        {
            _client = client;
            _userService = userService;
            _requestCreationService = requestCreationService;
        }

        public async Task<UserResponse> GetResponse(string apiKey, string userId, string expiresIn)
        {
            string request = _requestCreationService.CreateUsersGetRequest(apiKey, userId, expiresIn);

            string json = await _client.GetStringAsync(request);

            UserResponse response = JsonConvert.DeserializeObject<UserResponse>(json);

            return response;
        }

        public async Task<Shared.Models.User> GetUserFromResponse(UserResponse response)
        {
            return await _userService.GetUserByUserId(response.Response.First().Id.ToString());
        }
    }
}
