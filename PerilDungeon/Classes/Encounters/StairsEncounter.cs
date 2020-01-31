using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{

    //The stairs encounter doesn't take you back to the Exploration encounter, so it doesn't take up time.
    public class StairsEncounter : IEncounter
    {
        public string Title { get => "Staircase"; set { } }
        public string Description { get => "You have come upon a large staircase. You can ascend or descend, or ignore the stairs and carry on."; set { } }
        public Party Party { get; set; }

        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new StairsEncounterChoice("Ascend", StairsDirection.Ascend, Party.Depth != 1));
                choices.Add(new StairsEncounterChoice("Descend", StairsDirection.Descend, true));
                choices.Add(new StairsEncounterChoice("Ignore the Stairs", StairsDirection.Ignore, true));
                return choices;
            }
            set { }
        }
    }
}
