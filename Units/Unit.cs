using GamePrototype.Items.EconomicItems;

namespace GamePrototype.Units
{
    public abstract class Unit
    {
        private const int INVENTORY_SIZE = 3;
        private uint _health;
        private uint _maxHealth;
        protected uint BaseDamage; 
        protected Inventory Inventory; // инвентарь
        

        public string Name { get; private set; }
        public uint Health
        {
            get => _health;
            protected set => _health = value; // Math.Max(0, value);
        }

        public uint MaxHealth => _maxHealth;

        protected Unit(string name, uint health, uint maxHealth, uint baseDamage) 
        {
            Name = name;
            _health = health;
            _maxHealth = maxHealth;
            BaseDamage = baseDamage;
            Inventory = new Inventory(INVENTORY_SIZE);
        }

        public void ApplyDamage(uint damage) // нанести урон
        {
            var damageApplied = CalculateAppliedDamage(damage);

            ////if (_health < damageApplied || (_health - damageApplied) <= 0) 
            //if (_health >= damageApplied)
            //{
            //    //_health = 0;
            //    _health -= damageApplied;
            //}
            //else 
            //{
            //    //_health -= damageApplied;
            //    _health = 0;
            //}

            // ПРОВЕРКА ПЕРЕД ВЫЧИТАНИЕМ — это спасает от переполнения
            if (damageApplied >= _health)
            {
                _health = 0; // Гарантированно 0
            }
            else
            {
                _health -= damageApplied; // Тут безопасно, так как _health > damageApplied
            }

            DamageReceiveHandler();
        }

        protected abstract uint CalculateAppliedDamage(uint damage);
        
        protected virtual void DamageReceiveHandler() { }
        
        public abstract uint GetUnitDamage();

        public abstract void HandleCombatComplete();

        public virtual void AddItemToInventory(Item item) 
        {
            if (!Inventory.TryAdd(item)) 
            {
                Console.WriteLine($"Inventory of {Name} is full");
            }
        }

        public void AddItemsFromUnitToInventory(Unit unit)
        {
            for (int i = 0; i < unit.Inventory.Items.Count; i++) 
            {
                if (!Inventory.TryAdd(unit.Inventory.Items[i])) 
                {
                    //inventory is full
                    return;
                }
            }
        }
    }
}
