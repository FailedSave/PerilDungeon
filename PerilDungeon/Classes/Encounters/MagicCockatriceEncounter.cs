using System;
using System.Collections.Generic;
using System.Text;

namespace PerilDungeon.Classes.Encounters
{
    class MagicCockatriceEncounter: IEncounter
    {
        public string Title { get => "Spellcasting at the Cockatrices"; set { } }
        public string Description { get => "Who will hurl magic at the cockatrice flock?"; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new MagicCockatriceEncounterChoice(c));
                }

                return choices;
            }
            set { }
        }
    }
}
