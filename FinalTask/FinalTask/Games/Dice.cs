namespace FinalTask.Games
{
    using FinalTask.Exceptions;

    public struct Dice
    {
        private readonly int _min;
        private readonly int _max;

        private int _currentValue;

        public int Number => _currentValue;

        private static readonly Random _random = new Random();

        public Dice(int min, int max)
        {
            if (min < 1 || min > int.MaxValue)
                throw new WrongDiceNumberException(min, 1, int.MaxValue);
            if (max < 1 || max > int.MaxValue)
                throw new WrongDiceNumberException(max, 1, int.MaxValue);
            if (min > max)
                throw new WrongDiceNumberException(min, 1, max);

            _min = min;
            _max = max;

            _currentValue = _random.Next(_min, _max + 1);
        }

        public Dice Roll()
        {
            _currentValue = _random.Next(_min, _max + 1);
            return this;
        }
    }
}
