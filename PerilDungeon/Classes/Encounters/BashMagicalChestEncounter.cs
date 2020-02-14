using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters 
{
    public class BashMagicalChestEncounter : IEncounter
    {
        public string Title { get => "Bashing the Chest Open"; set { } }
        public string Description { get => "Who will bash it open?"; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new BashMagicalChestEncounterChoice(c));
                }

                return choices;
            }
            set { }
        }
    }
}
