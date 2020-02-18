using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class MerchantThieveryEncounter : IEncounter
    {
        public string Title { get => "Exiled Thief"; set { } }
        public string Description { get => "You encounter a worn-out thief who's also undergoing the Ordeal. He offers to show you some useful tricks for $250, which he intends to trade for some healing potions."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new MerchantBuyEncounterChoice(c, Party, 250, Effect, "You pay the old thief. He spends a few hours showing {0} some useful moves to handle the common traps and creatures in the Delve."));
                }
                choices.Add(new MessageOnlyEncounterChoice("Decline", "You decline the thief's offer. He sighs, looks you over avaraciously, then shakes his head and leaves."));
                return choices;
            }
            set { }
        }

        private void Effect(Character c)
        {
            c.Thievery += 2;
        }
    }
}
