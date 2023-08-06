
namespace VKPRApp.Services.RequestCreationServices
{
    public interface IRequestCreationService
    {
        string CreateUsersGetRequest(string apiKey, string userId, string expiresIn);

        string CreateCompleteTaskRequest(string apiKey, string userId, string taskType);

        string CreateGetUsersRequest(string apiKey);

        string CreateGetUserByIdRequest(string apiKey, string userId);

        string CreateAddUserRequest(string apiKey, string jsonUser);

        string CreateSetBankCardToNullRequest(string apiKey, string userId);

        string CreateUpdateRequest(string apiKey, string jsonUser);

        string CreateUpdateAutoRenewalRequest(string apiKey, string userId, bool value);

        string CreateWallGetRequest(string[] ids, Shared.Models.Task task);
    }
}
