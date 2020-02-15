using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class BadAirDeepEncounter : IEncounter
    {
        public string Title { get => "Bad Air"; set { } }
        public string Description { get => "This deep in the dungeon, the foul gases are more pervasive and more concentrated, making it harder to avoid them. You hastily look in all directions, but are unable to avoid another foul miasma from settling on your party."; set { } }
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

