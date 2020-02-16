namespace Kehyeedra3.Services.Models
{
    public class User
    {
        public ulong Id { get; set; } = 0;
        public string Avatar { get; set; } = null;
        public string Username { get; set; } = null;
        public long Money { get; set; } = 0;
        public ulong LastMine { get; set; } = 0;

        public bool GrantMoney(User bank, long amount)
        {
            if(bank.Money > amount)
            {
                Money += amount % bank.Money;
                bank.Money -= amount;
                return true;
            }

            return false;
        }
    }
}
