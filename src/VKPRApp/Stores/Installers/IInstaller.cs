
namespace VKPRApp.Stores.Installers
{
    public interface IInstaller
    {
        event Action IsLoadingChanged;

        bool IsLoading { get; set; }

        Task Install(string apiKey, string userId);
    }
}
