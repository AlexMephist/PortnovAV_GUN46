namespace FinalTask.Games
{
    using System;
    using System.Collections.Generic;

    public class DiceGame : CasinoGameBase
    {
        private List<Dice> _playerDices;
        private List<Dice> _computerDices;

        private readonly int _count;
        private readonly int _minValue;
        private readonly int _maxValue;

        public DiceGame(int count, int minValue, int maxValue)
        {
            if (count <= 0)
                
                throw new ArgumentOutOfRangeException(nameof(count), "The number of bones must be greater than zero.");

            _count = count;
            _minValue = minValue;
            _maxValue = maxValue;

            _playerDices = new List<Dice>();
            _computerDices = new List<Dice>();

            for (int i = 0; i < _count; i++)
            {
                _playerDices.Add(new Dice(_minValue, _maxValue));
            }

            for (int i = 0; i < _count; i++)
            {
                _computerDices.Add(new Dice(_minValue, _maxValue));
            }
        }

        protected override void FactoryMethod()
        {
            
        }

        public override void PlayGame()
        {
            
            if (_playerDices == null || _playerDices.Count == 0)
            {
                Console.WriteLine("Ошибка: списки костей не инициализированы.");
                return;
            }

            Console.WriteLine("--- We’re rolling the dice ---");

            for (int i = 0; i < _playerDices.Count; i++)
            {
                _playerDices[i] = _playerDices[i].Roll();
            }

            for (int i = 0; i < _computerDices.Count; i++)
            {
                _computerDices[i] = _computerDices[i].Roll();
            }

            int playerScore = 0;
            foreach (var d in _playerDices)
            {
                playerScore += d.Number;
            }

            int computerScore = 0;
            foreach (var d in _computerDices)
            {
                computerScore += d.Number;
            }

            Console.WriteLine("------------------");
            Console.WriteLine($" TOTAL: Player {playerScore} | Computer {computerScore}");

            if (playerScore > computerScore)
            {
                Console.WriteLine(" Victory!");
                OnWinInvoke();
            }
            else if (playerScore < computerScore)
            {
                Console.WriteLine(" Loss!");
                OnLooseInvoke();
            }
            else
            {
                Console.WriteLine(" Draw");
                OnDrawInvoke();
            }
        }
    }
}
