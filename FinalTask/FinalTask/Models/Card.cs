namespace FinalTask.Models
{
    using FinalTask.Enums;

    public struct Card
    {
        
        public readonly CardSuit Suit { get; }

        public readonly CardValue Value { get; }

        public Card(CardSuit suit, CardValue value)
        {
            
            Suit = suit;

            Value = value;
        }
    }
}
