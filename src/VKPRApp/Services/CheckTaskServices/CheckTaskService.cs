using System.Text.RegularExpressions;
using VKPRApp.Services.RequestCreationServices;
using VKPRApp.Services.VkUrlServices;

namespace VKPRApp.Services.CheckTaskServices
{
    public class CheckTaskService : ICheckTaskService
    {
        private readonly IVkUrlService _urlService;
        private readonly IRequestCreationService _requestCreationService;
        private readonly HttpClient _client;

        public CheckTaskService(IVkUrlService urlService, HttpClient client, IRequestCreationService requestCreationService)
        {
            _urlService = urlService;
            _client = client;
            _requestCreationService = requestCreationService;
        }

        public async Task<CheckTaskResult> CheckTask(Shared.Models.Task task, string userId)
        {
            string[] ids = _urlService.GetIdsFromUrl(task.PostUrl);

            string request = _requestCreationService.CreateWallGetRequest(ids, task);

            string content = await _client.GetStringAsync(request);

            if (content.Contains("error"))
                return CheckTaskResult.Failed;

            Regex regex = new Regex(@"\d{9}");

            MatchCollection matches = regex.Matches(content);

            return matches.Any(m => m.Value == userId) ? CheckTaskResult.Success : CheckTaskResult.Failed;
        }
    }
}
