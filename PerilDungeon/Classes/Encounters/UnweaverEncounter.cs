using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class UnweaverEncounter : IEncounter
    {
        public string Title { get => "Unweaver"; set { } }
        public string Description { get => "You encounter a small but dreaded monster: the Unweaver. It looks like a pillbug the size of a large dog, with menacing, twitching antennae. Rumors say its touch can turn matter to dust."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new FightUnweaverEncounterChoice());
                choices.Add(new FleeUnweaverEncounterChoice());
                return choices;
            }
            set { }
        }
    }
}
