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
            Statuses = new List<string>();
            CanAct = true;
            IsPlayer = false;
        }

        public string Name { get; set; }
        public Dictionary<string, int> Stats { get; set; }
        public List<string> Statuses { get; set; }
        public bool CanAct { get; set; }

        public bool IsPlayer { get; set; } //controls whether the player is referred to as "you" in messages

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
            if (!Statuses.Contains(newStatus))
            {
                Statuses.Add(newStatus);
                updateCanAct();
            }
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
