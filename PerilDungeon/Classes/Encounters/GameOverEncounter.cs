using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class GameOverEncounter : IEncounter
    {
        public string Title { get => "Hope Slips Away"; set { } }
        public string Description { get => "You tried and struggled, but you and your friends have all been transformed. None of you can free the others, and the three of you are now decorations for the denizens and traps of the Delve."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new GameOverEncounterChoice());
                return choices;
            }
            set { }
        }
    }
}
