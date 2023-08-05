using System.Net.Http.Json;
using VKPRApp.ApiModels;
using VKPRApp.Shared.Services;

namespace VKPRApp.Services.UserCreationServices
{
    public class UserCreationService : IUserCreationService
    {
        private readonly IUserService _userService;

        public UserCreationService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Shared.Models.User> CreateUser(string apiKey, Response response)
        {
            Shared.Models.VKUser vkUser = new Shared.Models.VKUser(apiKey, response.response[0].id.ToString());
            Shared.Models.User user = new Shared.Models.User(response.response[0].first_name, 100, response.response[0].last_name, null) { VKUser = vkUser };

            await _userService.AddUser(user);

            return user;
        }
    }
}
