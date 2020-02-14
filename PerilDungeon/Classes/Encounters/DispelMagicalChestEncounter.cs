using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class DispelMagicalChestEncounter : IEncounter
    {
        public string Title { get => "Dispelling the magical lock"; set { } }
        public string Description { get => "Who will dispel the lock?"; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new DispelMagicalChestEncounterChoice(c));
                }

                return choices;
            }
            set { }
        }
    }
}
