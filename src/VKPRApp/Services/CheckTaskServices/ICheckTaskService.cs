
namespace VKPRApp.Services.CheckTaskServices
{
    public enum CheckTaskResult
    {
        Success,
        Failed
    }

    public interface ICheckTaskService
    {
        Task<CheckTaskResult> CheckTask(Shared.Models.Task task, string userId);
    }
}
