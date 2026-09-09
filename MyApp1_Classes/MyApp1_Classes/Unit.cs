using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp1_Classes
{
    public class Unit
    {
        private float _health;
        private float _armor;

        private int _damage;


        // свойства

        public string Name { get; }

        public float Health => _health;

        public int Damage => _damage;

        public float Armor 
        { 
            get => (_armor); set => _armor = value;  
        }
        
          
        
        // конструкторы

        public Unit(): this(name: "Unknown Unit") 
        { 
        }

        public Unit(string name)
        {
            Name = name;
        }

        public float GetRealHealth()
        { 
            Armor = 0.6f;
            return Health * (1f + Armor);
        }

        public bool SetDamage(float Health, float Armor, float value)
        {
            _damage = 5;
            Health = Health - value * Armor;
            _health = Health;

            if (Health <= 0f)
            {
                return true;
            }
            else
            {
                return false;
            }

           
        }
    }
}
