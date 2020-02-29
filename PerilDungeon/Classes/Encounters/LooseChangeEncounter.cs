using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class LooseChangeEncounter : IEncounter
    {
        public string Title { get => "Petrified Adventurer"; set { } }
        public string Description { get => "At a dead end you find the statue of a previous explorer—a young rogue who must have been thoroughly outmatched. The statue is carrying nothing valuable or useful except for a small coin purse."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new LooseChangeEncounterChoice());
                return choices;
            }
            set { }
        }
    }
}
