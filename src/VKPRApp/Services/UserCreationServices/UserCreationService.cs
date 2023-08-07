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

        public async Task<Shared.Models.User> CreateUser(string apiKey, UserResponse response)
        {
            Shared.Models.VKUser vkUser = new Shared.Models.VKUser(apiKey, response.Response[0].Id.ToString());
            Shared.Models.User user = new Shared.Models.User(response.Response[0].FirstName, 100, response.Response[0].LastName, null) { VKUser = vkUser };

            await _userService.AddUser(user);

            return user;
        }
    }
}
