using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class FirstEncounter : IEncounter
    {
        public string Title { get => "Into the Delve"; set { } }
        public string Description { get => "Heavy wooden doors close behind you, and guards take their place to ensure that you don't escape. You have until sunset 3 days from now to return with a Sunstone shard, or you will be left to certain doom."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new MessageOnlyEncounterChoice("Onward!", "You grit your teeth and follow Johanna down the corridor..."));
                return choices;
            }
            set { }
        }
    }
}
