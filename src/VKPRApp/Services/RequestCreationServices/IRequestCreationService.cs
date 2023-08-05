
namespace VKPRApp.Services.RequestCreationServices
{
    public interface IRequestCreationService
    {
        string CreateUsersGetRequest(string apiKey, string userId, string expiresIn);

        string CreateWallGetRequest(string[] ids, Shared.Models.Task task);
    }
}
