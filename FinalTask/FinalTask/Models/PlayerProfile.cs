namespace FinalTask.Models
{
    public class PlayerProfile
    {
        
        public string Name { get; }

        public long Bank { get; set; }

        private const long MaxBank = 1000000000;

        public PlayerProfile(string name)
        {
            
            if (string.IsNullOrWhiteSpace(name))
            {
                
                throw new ArgumentException("The player's name cannot be empty.");
            }

            Name = name;

            Bank = 1000;
        }

        public long PlaceBet(long amount)
        {
            
            if (amount <= 0)
            {
                
                throw new ArgumentOutOfRangeException(nameof(amount), "The rate must be greater than zero.");
            }

            if (amount > Bank)
            {
                
                throw new System.InvalidOperationException("There are not enough funds for the bet.");
            }

            Bank -= amount;

            return amount;
        }

        public void WinMoney(long amount)
        {
            
            Bank += amount;

            if (Bank > MaxBank)
            {
                
                Bank = MaxBank;

                Console.WriteLine("You ruined the casino! A new one will be built in its place.");
            }
        }

        public void WasteHalfBank()
        {
            
            Bank /= 2;

            Console.WriteLine("You wasted half of your bank money in casino’s bar");
        }
    }
}
