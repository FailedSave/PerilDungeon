using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes
{
    public class Character
    {
        public Character(string name)
        {
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
        public int Knavery { get; set; }
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
    }
}
