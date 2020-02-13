using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class BackstabGorgonEncounter : IEncounter
    {
        public string Title { get => "Assassinating the Gorgon"; set { } }
        public string Description { get => "Who will sneak up on the gorgon to dispatch her from behind?"; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new BackstabGorgonEncounterChoice(c));
                }
                return choices;
            }
            set { }
        }
    }
}
