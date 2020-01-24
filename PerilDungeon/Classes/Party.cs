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

        public Party()
        {
            PartyMembers = new List<Character>();
            rng = new Random();
        }

        public Character GetRandomCharacter()
        {
            return PartyMembers[rng.Next(0, PartyMembers.Count)];
        }

        public bool HasCharacterActive
        {
            get
            { //TODO
                return true;
            }
        }
    }
}
