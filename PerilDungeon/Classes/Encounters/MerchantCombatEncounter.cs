using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class MerchantCombatEncounter : IEncounter
    {
        public string Title { get => "Banished Guard"; set { } }
        public string Description { get => "You encounter a lieutenant on the city guard. Although a devout follower of Kylan, she was framed of crimes by a political rival; she went to the Delve to prove her innocence in her god's eyes. She offers to teach you some useful combat moves for gems worth $250."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new MerchantBuyEncounterChoice(c, Party, 250, Effect, "You pay the guard. She spends a few hours showing {0} how to best use the light weapons at hand to dispatch the foes found in the Delve."));
                }
                choices.Add(new MessageOnlyEncounterChoice("Decline", "You decline the guard's offer. She wishes you luck and offers you a blessing before you leave."));
                return choices;
            }
            set { }
        }

        private void Effect(Character c)
        {
            c.Combat += 2;
        }
    }
}
