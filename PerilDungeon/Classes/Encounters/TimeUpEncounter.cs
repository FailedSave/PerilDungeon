using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class TimeUpEncounter : IEncounter
    {
        public string Title { get => "Time Has Run Out"; set { } }
        public string Description { get => "The three days you were given have run out. The guards abandon you for lost and reseal the entrance. You carry on as best you can, but are eventually overwhelmed by the dangers of the Delve."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new TimeUpEncounterChoice());
                return choices;
            }
            set { }
        }
    }
}
