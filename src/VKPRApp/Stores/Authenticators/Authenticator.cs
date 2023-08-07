using VKPRApp.ApiModels;
using VKPRApp.Services.ResponseServices;
using VKPRApp.Services.UserCreationServices;
using VKPRApp.Shared.Services;

namespace VKPRApp.Stores.Authenticators
{
    class Authenticator : IAuthenticator
    {
        public event Action UserChanged;

        private readonly IResponseService _responseService;
        private readonly IUserService _userService;
        private readonly IUserCreationService _userCreationService;

        public Authenticator(IUserService userService, IResponseService responseService, IUserCreationService userCreationService)
        {
            _userService = userService;
            _responseService = responseService;
            _userCreationService = userCreationService;
        }

        private Shared.Models.User _currentUser;

        public Shared.Models.User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                UserChanged?.Invoke();
            }
        }

        public async Task LoginAsync(string apiKey, string userId)
        {
            UserResponse response = await _responseService.GetResponse(apiKey, userId, "86400");

            Shared.Models.User? user = await _responseService.GetUserFromResponse(response);

            if (user is null)
            {
                CurrentUser = await _userCreationService.CreateUser(apiKey, response);
            }
            else
            {
                user.VKUser.ApiKey = apiKey;
                CurrentUser = await _userService.UpdateUser(user);
            }

            await Shell.Current.GoToAsync("//TasksPage");
        }
    }
}
