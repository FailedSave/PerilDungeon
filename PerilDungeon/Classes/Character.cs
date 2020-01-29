using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes
{
    public class Character
    {
        private Random rng;
        public Character(string name)
        {
            rng = new Random();
            Name = name;
            Stats = new Dictionary<string, int>();
            Statuses = new HashSet<string>();
            CanAct = true;
            IsPlayer = false;
            Level = 1;
            XP = 0;
        }

        public string Name { get; set; }
        public Dictionary<string, int> Stats { get; set; }
        public HashSet<string> Statuses { get; set; }
        public bool CanAct { get; set; }

        public bool IsPlayer { get; set; } //controls whether the player is referred to as "you" in messages

        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Mana { get; set; }
        public int MaxMana { get; set; }
        public int Combat { get; set; }
        public int Thievery { get; set; }
        public int Level { get; set; }
        public int XP { get; set; }

        public string Image
        {
            get
            {
                if (Statuses.Contains("Petrified"))
                {
                    return "assets/" + Name.ToLower() + "-stone.png";
                }
                return "assets/" + Name.ToLower() + ".png";
            }
        }

        public void AddStatus(string newStatus)
        {
            Statuses.Add(newStatus);
            updateCanAct();
        }

        public bool HasStatus (string status)
        {
            return Statuses.Contains(status);
        }

        private void updateCanAct()
        {//TODO
            CanAct = !(HasStatus("Petrified"));
        }
        
        public void Rest()
        {
            //Can't recover health if you're stone
            if (HasStatus("Petrified"))
            {
                return;
            }
            int recovery = rng.Next(1, Level + 1);
            recovery += rng.Next(1, 1 + (MaxHealth - Health)) / 10;
            Health = Math.Min(MaxHealth, Health + recovery);

            recovery = rng.Next(1, Level + 1);
            recovery += rng.Next(1, 1 + (MaxMana - Mana)) / 10;
            Mana = Math.Min(MaxMana, Mana + recovery);
        }

        public void LoseHealth(int amount)
        {
            Health = Math.Max(0, Health - amount);
        }

        public double HealthRatio { get => (double)Health / (double)MaxHealth; }
    }
}
