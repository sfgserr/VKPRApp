
namespace VKPRApp.Services.UserActionServices
{
    public interface IUserActionService
    {
        event Action ActionMade;

        void Subscribe();

        void LinkBankCard(Shared.Models.BankCard bankCard);
    }
}
