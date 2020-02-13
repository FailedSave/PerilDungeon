using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class GelatinousCubeEncounter : IEncounter
    {
        public string Title { get => "Acidic Cube"; set { } }
        public string Description { get => "You hear it before you spot it: a slurping sound of a transparent acidic jelly, sliding through the corridors, is almost upon you. If you turn and sprint away, you can probably escape. On the other hand, you can take out your weapons and try to defeat it."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new FightGelatinousCubeEncounterChoice());
                choices.Add(new FleeGelatinousCubeEncounterChoice());
                return choices;
            }
            set { }
        }
    }
}
