using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;


namespace GamePrototype.Utils
{
    public class HardUnitFactory : UnitFactory
    {
        public override Unit CreatePlayer(string name)
        {
            var player = new Player(name, 20, 20, 4);

            player.AddItemToInventory(new Weapon(7, 10, "Sword"));
            player.AddItemToInventory(new Armour(7, 10, "Armour"));
            player.AddItemToInventory(new RangeWeapon(7, 10, "Range Weapon"));
            player.AddItemToInventory(new Helmet(7, 10, "Helmet"));
            player.AddItemToInventory(new HealthPotion("Potion"));
            player.AddItemToInventory(new Grindstone("Grindstone"));
            return player;
        }

        public override Unit CreateGoblinEnemy() => new Goblin(GameConstants.Goblin, 18, 18, 4);

    }
    
}
