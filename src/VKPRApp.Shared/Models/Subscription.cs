
namespace VKPRApp.Shared.Models
{
    public class Subscription : DomainObject
    {
        public Subscription(DateTime expireDate, bool autoRenewal)
        {
            ExpireDate = expireDate;
            AutoRenewal = autoRenewal;
        }

        public DateTime ExpireDate { get; set; }
        public bool AutoRenewal { get; set; }
    }
}
