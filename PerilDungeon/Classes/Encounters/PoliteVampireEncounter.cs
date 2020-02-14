using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class PoliteVampireEncounter : IEncounter
    {
        public string Title { get => "Polite Vampire"; set { } }
        public string Description { get => "Your party is approached by a vampire, but instead of attacking, the undead greets you politely. She is desperately thirsty but has sworn off violence, and offers to pay one of your party members in exchange for a drink of life-sustaining blood."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new AcceptVampireEncounterChoice(c));
                }
                choices.Add(new MessageOnlyEncounterChoice("Decline", "You decline the vampire's offer. She frowns, but waves graciously and leaves peacefully."));
                return choices;
            }
            set { }
        }
    }
}
