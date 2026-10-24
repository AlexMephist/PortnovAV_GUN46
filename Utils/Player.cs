using GamePrototype.Combat;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;
using System.Text;

namespace GamePrototype.Units
{
    public sealed class Player : Unit
    {
        private readonly Dictionary<EquipSlot, EquipItem> _equipment = new();

        private AttackMode _currentAttackMode = AttackMode.Melee;

        public Player(string name, uint health, uint maxHealth, uint baseDamage) : base(name, health, maxHealth, baseDamage)
        {
        }

        public void ToggleAttackMode()
        {
            _currentAttackMode = _currentAttackMode == AttackMode.Melee
                ? AttackMode.Ranged
                : AttackMode.Melee;
            Console.WriteLine($"[INFO] Attack mode switched to: {_currentAttackMode}");
        }

        public void SetAttackMode(AttackMode mode)
        {
            _currentAttackMode = mode;
            Console.WriteLine($"[INFO] Attack mode set to: {_currentAttackMode} (for this fight)");
        }

        public override uint GetUnitDamage()
        {

            uint damage = BaseDamage;

            switch (_currentAttackMode)
            {
                case AttackMode.Melee:
                    if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon weapon)
                    {
                        damage += weapon.Damage;
                    }
                    break;

                case AttackMode.Ranged:
                    if (_equipment.TryGetValue(EquipSlot.RangeWeapon, out var item2) && item2 is RangeWeapon weapon2)
                    {
                        damage += weapon2.Damage;
                    }
                    break;
            }

            return damage;
        }

        public override void HandleCombatComplete()
        {
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++)
            {
                if (i >= items.Count) break;
                
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
                int restore = (int)healthPotion.HealthRestore;
                int newHealth = (int)Health + restore;

                // Не даём превысить максимум
                if (newHealth > (int)MaxHealth)
                {
                    newHealth = (int)MaxHealth;
                }

                Health = (uint)newHealth;
                Console.WriteLine($"{Name} used potion. HP: {Health}/{MaxHealth}");
            }

            if (economicItem is Grindstone grindStone)
            {
                bool repairedAny = false;

                if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is EquipItem equip)
                {
                    equip.Repair(4);
                    repairedAny = true;
                    Console.WriteLine("Grindstone used: Weapon repaired.");
                }

                if (_equipment.TryGetValue(EquipSlot.RangeWeapon, out var rangeItem) && rangeItem is EquipItem rangeEquip)
                {
                    rangeEquip.Repair(4);
                    repairedAny = true;
                    Console.WriteLine("Grindstone used: RangeWeapon repaired.");
                }

                if (repairedAny)
                {
                    Inventory.TryRemove(economicItem);
                    Console.WriteLine("Grindstone consumed.");
                }
            }
            
        }

        protected override uint CalculateAppliedDamage(uint damage)
        {
            uint finalDamage = damage;

            // Сначала шлем
            if (_equipment.TryGetValue(EquipSlot.Helmet, out var helmetItem) && helmetItem is Helmet helmet)
            {
                
                float reductionPercent = helmet.Defence / 100f;
                uint reduction = (uint)(finalDamage * reductionPercent);

                if (reduction > finalDamage)
                    reduction = finalDamage;

                finalDamage -= reduction;

                helmet.ReduceDurability(1);

                if (helmet.Durability == 0)
                {
                    _equipment.Remove(EquipSlot.Helmet);
                    Console.WriteLine($"{Name}'s helmet broke!");
                }
            }

            if (finalDamage > 0 && _equipment.TryGetValue(EquipSlot.Armour, out var armourItem) && armourItem is Armour armour)
            {
                float reductionPercent = armour.Defence / 100f;
                uint reduction = (uint)(finalDamage * reductionPercent);

                if (reduction > finalDamage)
                    reduction = finalDamage;

                finalDamage -= reduction;

                armour.ReduceDurability(1);

                if (armour.Durability == 0)
                {
                    _equipment.Remove(EquipSlot.Armour);
                    Console.WriteLine($"{Name}'s armour broke!");
                }
            }

            return finalDamage > 0 ? finalDamage : 0;
            
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
