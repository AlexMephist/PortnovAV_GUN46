using GamePrototype.Combat;
using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;
using GamePrototype.Utils;
using System.Numerics;

namespace GamePrototype.Game
{
    public sealed class GameLoop
    {
        private Unit _player;
        private DungeonRoom _dungeon;
        private readonly CombatManager _combatManager = new CombatManager();
        
        public void StartGame() 
        {
            Initialize();
            Console.WriteLine("Entering the dungeon");
            StartGameLoop();
        }

        #region Game Loop

        private void Initialize()
        {
            
            Console.WriteLine("Welcome, player!");
            Console.WriteLine("Choose difficulty: 0 - Easy, 1 - Hard");

            if (Enum.TryParse<LevelSelection>(Console.ReadLine(), out var levelSelection))
            {
                switch (levelSelection)
                {

                    case LevelSelection.Easy:

                        EasyDungeonBuilder easyDungeonBuilder = new();
                        _dungeon = easyDungeonBuilder.BuildDungeon();
                        Console.WriteLine("Enter your name");
                        EasyUnitFactory unitFactory = new();
                        _player = unitFactory.CreatePlayer(Console.ReadLine());
                        Console.WriteLine($"Hello {_player.Name}");
                        
                        return;

                    case LevelSelection.Hard:

                        HardDungeonBuilder hardDungeonBuilder = new();
                        _dungeon = hardDungeonBuilder.BuildDungeon();
                        Console.WriteLine("Enter your name");
                        HardUnitFactory unitFactory2 = new();
                        _player = unitFactory2.CreatePlayer(Console.ReadLine());
                        Console.WriteLine($"Hello {_player.Name}");
                        
                        return;

                }
            }
            
                  
        }

        private void StartGameLoop()
        {
            var currentRoom = _dungeon;
            
            while (currentRoom.IsFinal == false) 
            {
                StartRoomEncounter(currentRoom, out var success);
                if (!success) 
                {
                    Console.WriteLine("Game over!");
                    return;
                }
                DisplayRouteOptions(currentRoom);
                while (true) 
                {
                    if (Enum.TryParse<Direction>(Console.ReadLine(), out var direction) ) 
                    {
                        currentRoom = currentRoom.Rooms[direction];
                        break;
                    }
                    else 
                    {
                        Console.WriteLine("Wrong direction!");
                    }
                }
            }
            Console.WriteLine($"Congratulations, {_player.Name}");
            Console.WriteLine("Result: ");
            Console.WriteLine(_player.ToString());
        }

        private void StartRoomEncounter(DungeonRoom currentRoom, out bool success)
        {
            success = true;
            if (currentRoom.Loot != null) 
            {
                _player.AddItemToInventory(currentRoom.Loot);
            }
            if (currentRoom.Enemy != null) 
            {
                if (_combatManager.StartCombat(_player, currentRoom.Enemy) == _player)
                {
                    
                    LootEnemy(currentRoom.Enemy);
                }
                else 
                {
                    success = false;
                }
            }

            void LootEnemy(Unit enemy)
            {
                _player.AddItemsFromUnitToInventory(enemy);
            }
        }

        private void DisplayRouteOptions(DungeonRoom currentRoom)
        {
            Console.WriteLine("Where to go?");
            foreach (var room in currentRoom.Rooms)
            {
                Console.Write($"{room.Key} - {(int) room.Key}\t");
            }
        }

        
        #endregion
    }
}
