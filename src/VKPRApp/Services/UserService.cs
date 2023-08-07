using Newtonsoft.Json;
using System.Net.Http.Json;
using VKPRApp.Services.RequestCreationServices;
using VKPRApp.Shared.Services;

namespace VKPRApp.Services
{
    class UserService : IUserService
    {
        const string _accessToken = "askdjhAKSHkjdhsjkasm.12ASHdjjsd";

        private readonly IRequestCreationService _requestCreationService;
        private readonly HttpClient _client;

        public UserService(HttpClient client, IRequestCreationService requestCreationService)
        {
            _client = client;
            _requestCreationService = requestCreationService;
        }

        public async Task AddUser(Shared.Models.User user)
        {
            string json = JsonConvert.SerializeObject(user);

            string requestUri = _requestCreationService.CreateAddUserRequest(_accessToken, json);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            
            await _client.SendAsync(request);
        }

        public async Task<Shared.Models.User> CompleteTask(Shared.Models.TaskType taskType, string userId)
        {
            int taskTypeNum = (int)taskType;

            string requestUri = _requestCreationService.CreateCompleteTaskRequest(_accessToken, userId, taskTypeNum.ToString());

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, requestUri);

            HttpResponseMessage responseMessage = await _client.SendAsync(request);
            string result = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Shared.Models.User>(result);
        }

        public async Task<Shared.Models.User> GetUserByUserId(string userId)
        {
            try
            {
                string requestUri = _requestCreationService.CreateGetUserByIdRequest(_accessToken, userId);
                return await _client.GetFromJsonAsync<Shared.Models.User>(requestUri);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Shared.Models.User>> GetUsers()
        {
            string requestUri = _requestCreationService.CreateGetUsersRequest(_accessToken);
            return await _client.GetFromJsonAsync<List<Shared.Models.User>>(requestUri);
        }

        public async Task<Shared.Models.User> SetBankCardToNull(string userId)
        {
            string requestUri = _requestCreationService.CreateSetBankCardToNullRequest(_accessToken, userId);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            HttpResponseMessage response = await _client.SendAsync(request);

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Shared.Models.User>(json);
        }

        public async Task<Shared.Models.User> UpdateAutoRenewal(string userId, bool value)
        {
            string requestUri = _requestCreationService.CreateUpdateAutoRenewalRequest(_accessToken, userId, value);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            HttpResponseMessage response = await _client.SendAsync(request);

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Shared.Models.User>(json);
        }

        public async Task<Shared.Models.User> UpdateUser(Shared.Models.User user)
        {
            string json = JsonConvert.SerializeObject(user);

            string requestUri = _requestCreationService.CreateUpdateRequest(_accessToken, json);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            HttpResponseMessage response = await _client.SendAsync(request);

            string resultJson = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Shared.Models.User>(resultJson);
        }
    }
}
