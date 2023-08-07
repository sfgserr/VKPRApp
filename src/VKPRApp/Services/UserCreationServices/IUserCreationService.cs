using VKPRApp.ApiModels;

namespace VKPRApp.Services.UserCreationServices
{
    public interface IUserCreationService
    {
        Task<Shared.Models.User> CreateUser(string apiKey, UserResponse response);
    }
}
