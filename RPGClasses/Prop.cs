using System;

namespace RPGClasses
{
    public class Prop
    {
        public int Health;
        public bool Destroyed;
        public string Name;
        public Prop(string name, int health)
        {
            Name = name;
            Health = health;
            Destroyed = false;
        }
    }
}
