
namespace VKPRApp.Services.RequestCreationServices
{
    public interface IRequestCreationService
    {
        string CreateUsersGetRequest(string apiKey, string userId, string expiresIn);

        string CreateCompleteTaskRequest(string apiKey, string userId, string taskType);

        string CreateWallGetRequest(string[] ids, Shared.Models.Task task);
    }
}
