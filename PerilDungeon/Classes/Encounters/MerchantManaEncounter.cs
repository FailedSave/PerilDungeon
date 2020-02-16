using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class MerchantManaEncounter : IEncounter
    {
        public string Title { get => "Covetous Faerie"; set { } }
        public string Description { get => "You come upon a faerie who's also exploring the delve. He offers to sell you a dose of faerie powder, a powerful reagent which will restore a party member's mana, for $125."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new MerchantBuyEncounterChoice(c, Party, 125, Effect, "You pay the faerie. He chuckles and blows a large puff of faerie dust over {0}. It's rude, but it has the promise effect of completely revitalizing {1} magic."));
                }
                choices.Add(new MessageOnlyEncounterChoice("Decline", "You decline the faerie's offer. He rolls his eyes and flutters away."));
                return choices;
            }
            set { }
        }

        private void Effect(Character c)
        {
            c.GainMana(10000);
        }
    }
}
