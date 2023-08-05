using VKPRApp.ApiModels;

namespace VKPRApp.Services.ResponseServices
{
    public interface IResponseService
    {
        Task<Response> GetResponse(string apiKey, string userId, string expiresIn);

        Task<Shared.Models.User> GetUserFromResponse(Response response);
    }
}
