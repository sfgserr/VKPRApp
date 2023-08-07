
namespace VKPRApp.Shared.Services
{
    public interface IUserService
    {
        Task<Models.User> GetUserByUserId(string userId);

        Task<List<Models.User>> GetUsers();

        Task AddUser(Models.User user);

        Task<Shared.Models.User> UpdateUser(Models.User user);

        Task<Models.User> CompleteTask(Models.TaskType taskType, string userId);

        Task<Models.User> SetBankCardToNull(string userId);

        Task<Models.User> UpdateAutoRenewal(string userId, bool value);
    }
}
