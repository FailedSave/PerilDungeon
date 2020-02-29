using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class DeckOfWondersEncounter : IEncounter
    {
        public string Title { get => "Deck of Wonders"; set { } }
        public string Description { get => "You encounter a soot-stained halfling stumbling through a doorway. In his hand is a deck of Tarot cards with the mask icon of Jushalan on the back. \"My friends and I were playing poker with this deck. We got sticking rich and then engulfed by a fireball, all at once. We're done with this game; it's yours if you want it.\""; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new DeckOfWondersDrawEncounterChoice());
                choices.Add(new MessageOnlyEncounterChoice("Decline", "\"That's probably wise,\", says the hobbit, clutching his scorched wounds as he leaves."));

                return choices;
            }
            set { }
        }

    }
}
