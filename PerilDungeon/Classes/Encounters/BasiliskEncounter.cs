using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class BasiliskEncounter : IEncounter
    {
        public string Title { get => "Prowling Basilisk"; set { } }
        public string Description { get => "Your party comes across a deadly basilisk wandering through the hallways. Although small, these monsters are feared for their petrifying gaze."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new FightBasiliskEncounterChoice(c));
                }
                choices.Add(new FleeBasiliskEncounterChoice());
                return choices;
            }
            set { }
        }
    }
}
