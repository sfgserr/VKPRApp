
namespace VKPRApp.Shared.Models
{
    public class BankCard : DomainObject
    {
        public BankCard(string cardNumber, string expireDate, string cvv)
        {
            CardNumber = cardNumber;
            ExpireDate = expireDate;
            Cvv = cvv;
        }

        public string CardNumber { get; set; } = string.Empty;
        public string ExpireDate { get; set; } = string.Empty;
        public string Cvv { get; set; } = string.Empty;
    }
}
