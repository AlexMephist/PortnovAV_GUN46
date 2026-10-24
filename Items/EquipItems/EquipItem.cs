using GamePrototype.Items.EconomicItems;
using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public abstract class EquipItem : Item
    {
        private uint _durability;
        private uint _maxDurability;
        public uint Durability { get => _durability; protected set => _durability = value; }

        public override bool Stackable => false;

        public abstract EquipSlot Slot { get; }

        protected EquipItem(uint durability, string name) : base(name)
        {
            _maxDurability = durability;
            _durability = durability;
        }

        public void ReduceDurability(uint delta)
        {
            if (delta >= _durability)
                _durability = 0;
            
            else
                _durability -= delta;
        }
        public void Repair(uint delta)
        {
            if (delta == 0)
                return;

            if (_durability + delta >= _maxDurability)
            {
                _durability = _maxDurability;
            }
            else
            {
                _durability += delta;
            }
        }
    }
}
