using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class FightGorgonEncounter : IEncounter
    {
        public string Title { get => "Fighting the Gorgon"; set { } }
        public string Description { get => "Who will close her eyes and try to slay the gorgon?"; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new FightGorgonEncounterChoice(c));
                }

                return choices;
            }
            set { }
        }
    }
}
