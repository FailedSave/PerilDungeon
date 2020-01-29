using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class BadAirEncounter : IEncounter
    {
        public string Title { get => "Bad Air"; set { } }
        public string Description { get => "As you explore an unknown passage, dark purple gas begins seeping through the cracks. You try to backtrack quickly, but aren't able to avoid all the gas."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new BadAirEncounterChoice());
                return choices;
            }
            set { }
        }
    }
}
