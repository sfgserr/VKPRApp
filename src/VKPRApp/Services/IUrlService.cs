
namespace VKPRApp.Services
{
    public interface IUrlService
    {
        string GetFragmentByRegex(string url, string pattern);
    }
}
