using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class WyrdRunesEncounter : IEncounter
    {
        public string Title { get => "Wyrd Runes"; set { } }
        public string Description { get => "You come across ancient, magical runes, ornately carved into a cavern face. These runes are said to hold great power; that said, the magic within them is barely controlled, and sure to be dangerous in a place such as this."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new WyrdRunesEncounterChoice(c));
                }
                choices.Add(new MessageOnlyEncounterChoice("Decline", "You decide to avoid the dangerous runes and continue exploring the dungeon."));
                return choices;
            }
            set { }
        }
    }
}
