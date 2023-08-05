using System.Text.Json;
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

        public async Task<Response> GetResponse(string apiKey, string userId, string expiresIn)
        {
            string request = _requestCreationService.CreateUsersGetRequest(apiKey, userId, expiresIn);

            string json = await _client.GetStringAsync(request);

            return JsonSerializer.Deserialize<Response>(json);
        }

        public async Task<Shared.Models.User> GetUserFromResponse(Response response)
        {
            return await _userService.GetUserByUserId(response.response.First().id.ToString());
        }
    }
}
