using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;
using System.Text;

namespace GamePrototype.Units
{
    public sealed class Player : Unit
    {
        private readonly Dictionary<EquipSlot, EquipItem> _equipment = new();

        public Player(string name, uint health, uint maxHealth, uint baseDamage) : base(name, health, maxHealth, baseDamage)
        {
        }

        public override uint GetUnitDamage()
        {
            if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon weapon)
            { 
                return BaseDamage + weapon.Damage;
            }
            if (_equipment.TryGetValue(EquipSlot.RangeWeapon, out var item2) && item is RangeWeapon weapon2)
            {
                return BaseDamage + weapon2.Damage;
            }

            return BaseDamage;
        }

        public override void HandleCombatComplete()
        {
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] is EconomicItem economicItem)
                {
                    UseEconomicItem(economicItem);
                    Inventory.TryRemove(items[i]);
                }
            }
        }

        public override void AddItemToInventory(Item item)
        {
            if (item is EquipItem equipItem )
            { 
                if (_equipment.TryGetValue(equipItem.Slot, out var equippedItem))
                {
                    Console.WriteLine($"Do you want to change {equippedItem.Name}");
                    string? equipItemchoice = Console.ReadLine();
                    
                    if (equipItemchoice == "Yes") 
                    {
                        
                        base.AddItemToInventory(equippedItem);
                        _equipment[equipItem.Slot] = equipItem;
                        Console.WriteLine($" {equipItem.Slot} changed: now equipped {equippedItem.Name}");
                        return;
                    }
                    
                    base.AddItemToInventory(item);
                    return;
                }
                
                _equipment[equipItem.Slot] = equipItem;
                Console.WriteLine($" equipped {equipItem.Name} to slot: {equipItem.Name}");
                return;

            }
            base.AddItemToInventory(item);
        
        }

        private void UseEconomicItem(EconomicItem economicItem)
        {
            if (economicItem is HealthPotion healthPotion)
            {
                Health += healthPotion.HealthRestore;
            }
            if (economicItem is Grindstone grindStone)
            {
                if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon weapon)
                {

                    item.Repair(grindStone.grindstone);
                    Console.WriteLine("Grindstone was used");
                }
                
            }
        }

        protected override uint CalculateAppliedDamage(uint damage)
        {
            if (_equipment.TryGetValue(EquipSlot.Armour, out var item) && item is Armour armour)
            {
                damage -= (uint)(damage * (armour.Defence / 100f));
            }
            if (_equipment.TryGetValue(EquipSlot.Helmet, out var item2) && item is Armour armour2)
            {
                damage -= (uint)(damage * (armour2.Defence / 100f));
            }

            return damage;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Name);
            builder.AppendLine($"Health {Health}/{MaxHealth}");
            builder.AppendLine("Loot:");
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++)
            {
                builder.AppendLine($"[{items[i].Name}] : {items[i].Amount}");
            }
            return builder.ToString();
        }
    }
}
