namespace FinalTaskLogic.Casino
{
    using System;
    using System.Text.Json;
    using FinalTask.Games;
    using FinalTask.Interfaces;
    using FinalTask.Models;
    using FinalTask.Services;

    public class Casino : IGame
    {
        private PlayerProfile _player;

        private readonly ISaveLoadService<string> _saveLoadService;

        private BlackjackGame _blackjackGame;

        private DiceGame _diceGame;

        private const string ProfilesPath = "Profiles";

        public Casino()
        {
            _saveLoadService = new FileSystemSaveLoadService(ProfilesPath);

            _blackjackGame = new BlackjackGame(36);

            _diceGame = new DiceGame(2, 1, 6);
        }

        public void StartGame()
        {
            Console.WriteLine("Welcome to Final Task Casino!");

            LoadOrCreateProfile();

            if (_player.Bank <= 0)
            {
                Console.WriteLine("No money? Kicked!");

                SaveProfile();
                return; 
            }

            int gameChoice = GetGameChoice();

            long bet = GetBet();

            if (bet == 0)
            {
                Console.WriteLine("t was not possible to place a bet. The game is over!");
                SaveProfile(); 
                return;
            }

            SubscribeToGameEvents(gameChoice, bet);

            RunSelectedGame(gameChoice);

            Console.WriteLine("Thank you for the game! See you at the casino.");

            SaveProfile();
        }

        private void LoadOrCreateProfile()
        {
            Console.Write("Enter the player’s name: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                name = "Guest";
            }

            string savedData = _saveLoadService.LoadData(name);

            if (!string.IsNullOrEmpty(savedData))
            {
                try
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    _player = JsonSerializer.Deserialize<PlayerProfile>(savedData, options);

                    Console.WriteLine($"Profile {name} loaded. Your balance: {_player.Bank}");
                }
                catch
                {
                    _player = new PlayerProfile(name);
                    Console.WriteLine($"Profile loading error. A new profile has been created {name}.");
                }
            }
            else
            {
                _player = new PlayerProfile(name);
                Console.WriteLine($"A new profile has been created {name}. Your balance: {_player.Bank}");
            }
        }
        private int GetGameChoice()
        {
            Console.WriteLine("\nChoose a game:");
            Console.WriteLine("1 — Blackjack");
            Console.WriteLine("2 — Dice game");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && (choice == 1 || choice == 2))
            {
                return choice; 
            }

            Console.WriteLine("Invalid input. Blackjack is selected by default.");
            return 1;
        }

        private long GetBet()
        {
            Console.WriteLine($"\nYour balance: {_player.Bank}. Enter the bet amount:");

            string input = Console.ReadLine();

            if (long.TryParse(input, out long bet))
            {
                try
                {
                    return _player.PlaceBet(bet);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Betting error: {ex.Message}");
                }
            }

            try
            {
                return _player.PlaceBet(10);
            }
            catch
            {
                return 0;
            }
        }
        private void SubscribeToGameEvents(int gameChoice, long bet)
        {
            if (gameChoice == 1)
            {
                _blackjackGame.OnWin += () => HandleWin(bet);
                _blackjackGame.OnLoose += () => HandleLoose(bet);
                _blackjackGame.OnDraw += () => HandleDraw(bet);
            }
            else
            {
                _diceGame.OnWin += () => HandleWin(bet);
                _diceGame.OnLoose += () => HandleLoose(bet);
                _diceGame.OnDraw += () => HandleDraw(bet);
            }
        }
        private void HandleWin(long bet)
        {
            _player.WinMoney(bet * 2);
        }
        private void HandleLoose(long bet)
        {

        }
        private void HandleDraw(long bet)
        {
            _player.WinMoney(bet);
        }
        private void RunSelectedGame(int gameChoice)
        {
            if (gameChoice == 1)
            {
                _blackjackGame.PlayGame();
            }
            else
            {
                _diceGame.PlayGame();
            }
        }
        private void SaveProfile()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // игноририть регистр при чтении/записи
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull // не писать null-значения
            };

            string json = JsonSerializer.Serialize(_player, options); // игрока в JSON-строку

            _saveLoadService.SaveData(json, _player.Name);
        }
    }
}
