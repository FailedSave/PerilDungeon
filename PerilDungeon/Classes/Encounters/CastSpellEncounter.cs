using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class CastSpellEncounter : IEncounter
    {
        public string Title { get => "Casting a Spell"; set { } }
        public string Description { get => "Who will cast the spell?"; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    if (c.Spells.Count > 0)
                    {
                        choices.Add(new CharacterCastSpellEncounterChoice(c));
                    }
                }
                choices.Add(new MessageOnlyEncounterChoice("Never mind", "You decide not to cast a spell after all."));
                return choices;
            }
            set { }
        }
    }
}
