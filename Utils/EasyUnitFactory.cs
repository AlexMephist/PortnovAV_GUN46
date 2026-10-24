using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;


namespace GamePrototype.Utils
{
    public class EasyUnitFactory : UnitFactory
    {
        public override Unit CreatePlayer(string name)
        {
            var player = new Player(name, 30, 30, 6);
                
            player.AddItemToInventory(new Weapon(10, 15, "Sword"));
            player.AddItemToInventory(new Armour(10, 15, "Armour"));
            player.AddItemToInventory(new RangeWeapon(7, 10, "Range Weapon"));
            player.AddItemToInventory(new Helmet(7, 10, "Helmet"));
            player.AddItemToInventory(new HealthPotion("Potion"));
            player.AddItemToInventory(new Grindstone("Grindstone"));
            return player;
        }

        public override Unit CreateGoblinEnemy() => new Goblin(GameConstants.Goblin, 10, 10, 2);

    }
}
