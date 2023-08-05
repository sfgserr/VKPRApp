
namespace VKPRApp.Stores.Authenticators
{
    public interface IAuthenticator
    {
        event Action UserChanged;

        Shared.Models.User CurrentUser { get; set; }

        Task LoginAsync(string apiKey, string userId);
    }
}
