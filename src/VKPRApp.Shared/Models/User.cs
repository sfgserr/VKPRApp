
namespace VKPRApp.Shared.Models
{
    public class User : DomainObject
    {
        public User(string name, int wallet, string secondName, DateTime? nextPromote)
        {
            Name = name;
            Wallet = wallet;
            SecondName = secondName;
            NextPromote = nextPromote;
        }

        public string Name { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public int Wallet { get; set; } = 0;
        public int Stack { get; set; } = 0;
        public VKUser VKUser { get; set; }
        public DateTime? NextPromote { get; set; }
        public BankCard? BankCard { get; set; }
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
        public Subscription? Subscription { get; set; }
    }
}
