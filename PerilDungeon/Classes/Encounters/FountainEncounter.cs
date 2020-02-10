using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class FountainEncounter : IEncounter
    {
        public string Title { get => "Mysterious Spring"; set { } }
        public string Description { get => "Tucked away in an alcove, you find a small spring in a stone basin, water trickling slowly in from unseen cracks. The water glows with a faint magical light."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new FountainEncounterChoice(c));
                }
                choices.Add(new MessageOnlyEncounterChoice("Ignore it", "You pass the spring by without drinking from it."));
                return choices;
            }
            set { }
        }
    }
}
