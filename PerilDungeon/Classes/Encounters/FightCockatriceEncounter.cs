using System;
using System.Collections.Generic;
using System.Text;

namespace PerilDungeon.Classes.Encounters
{
    class FightCockatriceEncounter : IEncounter
    {
        public string Title { get => "Fighting the Cockatrices"; set { } }
        public string Description { get => "Who will take point fighting the cockatrice flock?"; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new FightCockatriceEncounterChoice(c));
                }

                return choices;
            }
            set { }
        }
    }
}
