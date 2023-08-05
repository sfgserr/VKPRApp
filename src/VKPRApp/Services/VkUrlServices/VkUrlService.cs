using System.Text.RegularExpressions;

namespace VKPRApp.Services.VkUrlServices
{
    public class VkUrlService : IVkUrlService
    {
        public string GetFragmentByRegex(string url, string pattern)
        {
            Regex regex = new Regex(pattern);

            Match match = regex.Match(url);

            return match.Success ? match.Value : "";
        }

        public string[] GetIdsFromUrl(string url)
        {
            Regex regex = new Regex(@"\d{9}_\d");
            Match match = regex.Match(url);

            return match.Success ? match.Value.Split("_") : throw new ArgumentException();
        }
    }
}
