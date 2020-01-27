using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes
{
    public interface IEncounterChoice
    {
        public string Text { get; set; }

        //What happens when the choice is selected. Party is required; character is optional (some choices involve a specific party member). Returns a list of messages.
        public Func<Party, Character, IEnumerable<string>> Choose { get; set; }

        public bool IsAvailable { get; set; }

        //Given the party and the encounter, gets the next encounter. Will be called only after Choose().
        public IEncounter GetNextEncounter(Party p, IEncounter encounter);
    }
}
