using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes
{
    public class Party
    {
        public List<Character> PartyMembers { get; set; }
        Random rng;

        public int TimeRemaining;
        public int Depth;
        public int Money;
        public bool GameOver;

        public Party()
        {
            PartyMembers = new List<Character>();
            rng = new Random();
            TimeRemaining = 1000;
            Depth = 1;
            Money = 0;
            GameOver = false;
        }

        public Character GetRandomCharacter()
        {
            return PartyMembers[rng.Next(0, PartyMembers.Count)];
        }
        public Character GetRandomActiveCharacter()
        {
            Character target = PartyMembers[rng.Next(0, PartyMembers.Count)];
            while (!target.CanAct)
            {
                target = PartyMembers[rng.Next(0, PartyMembers.Count)];
            }
            return target;
        }

        public bool HasCharacterActive
        {//TODO refactor
            get
            {
                foreach (Character c in PartyMembers)
                {
                    if (c.CanAct)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
