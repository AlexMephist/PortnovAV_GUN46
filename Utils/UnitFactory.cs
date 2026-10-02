using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

namespace GamePrototype.Utils
{
    public abstract class UnitFactory
    {
        private readonly LevelSelection levelSelection;
        private static Unit player;
        private static string choice;

        public static Unit CreatePlayer(string name)
        {
            var player = new Player(name, 30, 30, 6);

            Console.WriteLine("Choose the difficulty level: '0' - easy '1'- hard");

            if (Enum.TryParse<LevelSelection>(Console.ReadLine(), out var levelSelection))
            {

                switch (levelSelection)
                {

                    case LevelSelection.easy:
                        
                        Console.WriteLine("Choose the equipment: '1'- Sword & Armour '2'- RangeWeapon & Helmet");
                        choice = Console.ReadLine();

                        if (string.IsNullOrEmpty(choice))
                        {
                            if (choice == "1")
                            {
                                player.AddItemToInventory(new Weapon(10, 15, "Sword"));
                                player.AddItemToInventory(new Armour(10, 15, "Armour"));
                                player.AddItemToInventory(new HealthPotion("Potion"));
                                player.AddItemToInventory(new Grindstone("Grindstone"));
                                Console.WriteLine("You have chosen - Sword & Armour");


                            }
                            if (choice == "2")
                            {
                                player.AddItemToInventory(new RangeWeapon(10, 15, "Crossbow"));
                                player.AddItemToInventory(new Helmet(10, 15, "Helmet"));
                                player.AddItemToInventory(new HealthPotion("Potion"));
                                player.AddItemToInventory(new Grindstone("Grindstone"));
                                Console.WriteLine("You have chosen - RangeWeapon & Helmet");

                            }
                          
                        }
                        return player;
                    
                    case LevelSelection.hard:

                        Console.WriteLine("Choose the equipment: '1'- Sword & Armour '2'- RangeWeapon & Helmet");
                        _ = Console.ReadLine();

                        if (string.IsNullOrEmpty(choice))
                        { 
                            if (choice == "1")
                            {
                                player.AddItemToInventory(new Weapon(8, 10, "Sword"));
                                player.AddItemToInventory(new Armour(8, 10, "Armour"));
                                player.AddItemToInventory(new HealthPotion("Potion"));
                                player.AddItemToInventory(new Grindstone("Grindstone"));
                                Console.WriteLine("You have chosen - Sword & Armour");


                            }
                            if (choice == "2")
                            {
                                player.AddItemToInventory(new RangeWeapon(8, 10, "Crossbow"));
                                player.AddItemToInventory(new Helmet(8, 10, "Helmet"));
                                player.AddItemToInventory(new HealthPotion("Potion"));
                                player.AddItemToInventory(new Grindstone("Grindstone"));
                                Console.WriteLine("You have chosen - RangeWeapon & Helmet");
                            }
                        }
                        return player;

                }
            }

            return player;


        }


        public static Unit CreateGoblinEnemy() => new Goblin(GameConstants.Goblin, 18, 18, 2);
    }
}