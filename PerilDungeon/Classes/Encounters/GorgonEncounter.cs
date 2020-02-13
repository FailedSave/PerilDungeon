using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class GorgonEncounter : IEncounter
    {
        public string Title { get => "Lurking Gorgon"; set { } }
        public string Description { get => "Ahead in the passages you notice movement and hear the hissing of snakes; from the shadows you recognize the silhouette of a mythical gorgon. She's almost certainly heard you, but not spotted you yet. You can either try to fight her with your eyes closed, or try to sneak around and stab her unawares."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new FollowUpEncounterChoice("Fight her", "You hold your breath as one of your number closes her eyes and prepares to fight...", typeof(FightGorgonEncounter)));
                choices.Add(new FollowUpEncounterChoice("Assassinate her", "The party holds still and silent as one of you quietly skulks off to surprise the creature...", typeof(BackstabGorgonEncounter)));
                return choices;
            }
            set { }
        }
    }
}
