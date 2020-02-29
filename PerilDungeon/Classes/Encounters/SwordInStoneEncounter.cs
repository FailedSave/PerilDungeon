using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class SwordInStoneEncounter : IEncounter
    {
        public string Title { get => "Sword in the Stone"; set { } }
        public string Description { get => "You come across an strange monument: an ancient sword buried up to its hilt in a large boulder. You've heard stories of these old weapons, sleeping for decades, waiting only to be pulled and wielded by the worthy. Perhaps one of you is worthy?"; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new PullSwordEncounterChoice(c));
                }
                choices.Add(new MessageOnlyEncounterChoice("Leave", "Magical trials are more trouble than they're worth. You leave the old weapon for someone else to deal with."));
                return choices;
            }
            set { }
        }
    }
}
