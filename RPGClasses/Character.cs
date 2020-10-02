using System;
using System.Collections.Generic;
using System.Text;

namespace RPGClasses
{
    public class Character
    {
        public int Health;
        public int Level;
        public bool Alive;
        public int Damage;
        public int Heal;
        public int ID;
        public string Type;
        public int MaxRange;
        private Dictionary<string, int> Types = new Dictionary<string, int>
        {
            ["Melee"] = 2,
            ["Ranged"] = 20
        };
        public List<string> Factions = new List<string>();

        public Character(int id, string type)
        {
            Health = 1000;
            Level = 1;
            Alive = true;
            Damage = 20;
            Heal = 10;
            ID = id;
            Type = type;
            MaxRange = Types[type];
        }

        public void JoinFaction(string faction)
        {
            Factions.Add(faction);
        }

        public void LeaveFaction(string faction)
        {
            Factions.Remove(faction);
        }

        public bool Allies(Character character)
        {
            bool allies = false;
            List<String> factions = character.Factions;
            for (int i = 0; i < factions.Count; i++)
            {
                if (Factions.Contains(factions[i]))
                {
                    allies = true;
                }
            }
            return allies;
        }

        public void Attack(Character character, int distance)
        {
            if (character.ID == ID)
            {
                return;
            }
            if (Allies(character))
            {
                return;
            }
            int damage = Damage;
            if (character.Level >= 5 + Level)
            {
                damage = damage / 2;
            }
            else if (Level >= 5 + character.Level)
            {
                damage = damage + (damage/2);
            }
            // Attack the other Character if it is in range
            if (MaxRange >= distance)
            {
                character.Health -= damage;
            }
            if (character.Health <= 0)
            {
                character.Health = 0;
                character.Alive = false;
            }
        }

        public void AttackProp(Prop prop, int distance)
        {
            if (MaxRange >= distance && prop.Destroyed == false)
            {
                prop.Health -= Damage;
            }
            if (prop.Health <= 0)
            {
                prop.Health = 0;
                prop.Destroyed = true;
            }
        }

        public void HealCharacter(Character c)
        {
            if (c.ID == ID || Allies(c))
            {
                if (c.Alive)
                {
                    c.Health += Heal;
                }
                if (c.Health > 1000)
                {
                    c.Health = 1000;
                }
            }
        }
    }
}
