namespace FinalTask.Exceptions
{
    
    public class WrongDiceNumberException : Exception
    {
        
        public WrongDiceNumberException(string message)
            : base(message)
        {
            
        }
        public WrongDiceNumberException(int number, int min, int max)
            : base($"Incorrect value for the dice: {number}. Acceptable range: [{min}, {max}].")
        {
            
        }
    }
}
