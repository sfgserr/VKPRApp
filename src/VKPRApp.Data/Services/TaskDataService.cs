using VKPRApp.Shared.Services;

namespace VKPRApp.Data.Services
{
    public class TaskDataService : ITaskService
    {
        private readonly NonQueryDataService<Shared.Models.Task> _dataService;

        public TaskDataService(NonQueryDataService<Shared.Models.Task> dataService)
        {
            _dataService = dataService;
        }

        public async Task UpdateTask(Shared.Models.Task task)
        {
            await _dataService.Update(task);
        }
    }
}
