using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class ExplorationEncounter : IEncounter
    {
        public string Title { get => "Exploring"; set { } }
        public string Description { get => "You and your friends make your way through a series of twisty passages. You can press on, or rest for a while to recoup your strength."; set { } }
        public Party Party { get; set; }

        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new RestEncounterChoice());
                choices.Add(new ExploreEncounterChoice("Explore the Delve", true));
                //choices.Add(new ExploreEncounterChoice("Onward Boldly", false));
                return choices;
            }
            set { }
        }
    }
}
