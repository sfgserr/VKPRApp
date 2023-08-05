
namespace VKPRApp.Services.VkUrlServices
{
    public interface IVkUrlService : IUrlService
    {
        string[] GetIdsFromUrl(string url);
    }
}
