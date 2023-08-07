using VKPRApp.ApiModels;

namespace VKPRApp.Services.ResponseServices
{
    public interface IResponseService
    {
        Task<UserResponse> GetResponse(string apiKey, string userId, string expiresIn);

        Task<Shared.Models.User> GetUserFromResponse(UserResponse response);
    }
}
