using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;
using System.Numerics;

namespace GamePrototype.Utils
{
    public class UnitFactoryDemo
    {
        public static Unit CreatePlayer(string name)
        {
            var player = new Player(name, 30, 30, 6);
            Console.WriteLine("Choose the equipment: '1'- Sword & Armour '2'- RangeWeapon & Helmet");

            string? choice = Console.ReadLine();
                        
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
                 
                                   
            return player;
        }

        
        public static Unit CreateGoblinEnemy() => new Goblin(GameConstants.Goblin, 18, 18, 2);
    }
}
