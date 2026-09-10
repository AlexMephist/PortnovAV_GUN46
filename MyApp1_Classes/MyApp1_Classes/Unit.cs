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


        // свойства и конструкторы

        public string Name { get; }

        public float Health => _health;

        public int Damage
        {
            get 
            {
                return 5;
            }
        }
        

        public float Armor
        {
            get
            {
                return 0.6f;
            }
        }
         
             
              
        // методы

        public Unit(): this(name: "Unknown Unit") 
        { 
        }

        public Unit(string name)
        {
            Name = name;
        }

        public float GetRealHealth()
        { 
            return Health * (1f + Armor);
        }

        public bool SetDamage(float value)
        {
            
            _health = _health - value * Armor;
            
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
