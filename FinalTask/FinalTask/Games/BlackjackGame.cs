using System;
using System.Collections.Generic;
using System.Linq;
using FinalTask.Enums;
using FinalTask.Models;

namespace FinalTask.Games
{
    public class BlackjackGame : CasinoGameBase
    {
        private Queue<Card> _deck;

        private List<Card> _playerHand;
        private List<Card> _computerHand;

        public BlackjackGame(int cardsCountInDeck)
        {
            _playerHand = new List<Card>();
            _computerHand = new List<Card>();

            GenerateDeck(cardsCountInDeck);
            Shuffle();
        }

        protected override void FactoryMethod()
        {
            if (_deck == null || _deck.Count == 0)
            {
                GenerateDeck(36);
                Shuffle();
            }
        }

        public override void PlayGame()
        {
            Console.WriteLine("\n--- Game: Blackjack ---");

            _playerHand.Clear();
            _computerHand.Clear();

            DealCard(_playerHand);
            DealCard(_computerHand);
            DealCard(_playerHand);
            DealCard(_computerHand);

            ShowHands(showComputerSecondCard: false);

            bool playerBust = PlayerTurn();

            if (playerBust)
            {
                Console.WriteLine("The player has exceeded the limit! You have lost.");
                OnLooseInvoke();
                return;
            }

            ComputerTurn();

            ShowHands(showComputerSecondCard: true);

            int playerScore = CalculateHandScore(_playerHand);
            int computerScore = CalculateHandScore(_computerHand);

            Console.WriteLine($"Player score: {playerScore}");
            Console.WriteLine($"Computer score: {computerScore}");

            if (playerScore > computerScore || computerScore > 21)
            {
                Console.WriteLine("You win!");
                OnWinInvoke();
            }
            else if (playerScore < computerScore)
            {
                Console.WriteLine("You lost.");
                OnLooseInvoke();
            }
            else
            {
                Console.WriteLine("Draw.");
                OnDrawInvoke();
            }
        }

        private bool PlayerTurn()
        {
            while (true)
            {
                int score = CalculateHandScore(_playerHand);
                Console.WriteLine($"Your invoice: {score}. Would you like another card? (yes/no)");

                string input = Console.ReadLine()?.Trim().ToLower();
                if (input == "no" || input == "n" || input == "")
                {
                    break;
                }

                DealCard(_playerHand);
                ShowHands(showComputerSecondCard: false);

                score = CalculateHandScore(_playerHand);
                if (score > 21)
                {
                    return true; // перебор
                }
            }

            return false;
        }

        private void ComputerTurn()
        {
            Console.WriteLine("The computer opens the second card and draws the rest...");

            while (CalculateHandScore(_computerHand) < 17)
            {
                DealCard(_computerHand);
                
            }
        }

        private void DealCard(List<Card> hand)
        {
            if (_deck.Count == 0)
            {
                GenerateDeck(36);
                Shuffle();
            }

            var card = _deck.Dequeue();
            hand.Add(card); // добавляем в руку
        }

        private void GenerateDeck(int totalCards)
        {
            var deckList = new List<Card>();

            var suits = new[] { CardSuit.Diamonds, CardSuit.Hearts, CardSuit.Clubs, CardSuit.Spades };
            var values = new[]
            {
                CardValue.Six, CardValue.Seven, CardValue.Eight, CardValue.Nine,
                CardValue.Ten, CardValue.Jack, CardValue.Queen, CardValue.King, CardValue.Ace
            };

            foreach (var suit in suits)
            {
                foreach (var value in values)
                {
                    deckList.Add(new Card(suit, value));
                }
            }

            _deck = new Queue<Card>();
            while (_deck.Count < totalCards)
            {
                foreach (var card in deckList)
                {
                    if (_deck.Count >= totalCards) break;
                    _deck.Enqueue(card);
                }
            }
        }

        private void Shuffle() // перемешивание колоды
        {
            var list = _deck.ToList();
            var random = new Random();

            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }

            _deck = new Queue<Card>(list);
        }
                     
        private int CalculateHandScore(List<Card> hand) // туз считается как 11, но если это приводит к перебору — как 1.
        {
            int sum = 0;
            int aceCount = 0;

            foreach (var card in hand)
            {
                int value = GetCardValue(card);
                sum += value;

                if (card.Value == CardValue.Ace)
                {
                    aceCount++;
                }
            }
                        
            while (sum > 21 && aceCount > 0) // корректируем тузы - если перебор, уменьшаем значение тузов с 11 до 1 (разница 10)
            {
                sum -= 10;
                aceCount--;
            }

            return sum;
        }

        private int GetCardValue(Card card)
        {
            switch (card.Value)
            {
                case CardValue.Six: return 6;
                case CardValue.Seven: return 7;
                case CardValue.Eight: return 8;
                case CardValue.Nine: return 9;
                case CardValue.Ten:
                case CardValue.Jack:
                case CardValue.Queen:
                case CardValue.King: return 10;
                case CardValue.Ace: return 11; // считаем как 11
                default: return 0;
            }
        }

        private void ShowHands(bool showComputerSecondCard)
        {
            Console.Write("Your hand: ");
            foreach (var c in _playerHand)
            {
                Console.Write($"{c.Value}({c.Suit}) ");
            }
            Console.WriteLine();

            Console.Write("Computer hand: ");
            for (int i = 0; i < _computerHand.Count; i++)
            {
                if (!showComputerSecondCard && i == 1)
                {
                    Console.Write("[hidden] ");
                }
                else
                {
                    var c = _computerHand[i];
                    Console.Write($"{c.Value}({c.Suit}) ");
                }
            }
            Console.WriteLine();
        }
    }
}
