using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class CustomEncounterChoice : IEncounterChoice
    {
        public CustomEncounterChoice(string text, Func<Party, Character, IEnumerable<string>> choose)
        {
            Text = text;
            Choose = choose;
        }

        public string Text { get; set; }
        public Func<Party, Character, IEnumerable<string>> Choose { get; set; }
        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return new BasicEncounter();
        }
    }
}
