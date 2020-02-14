using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class MagicalChestEncounter : IEncounter
    {
        public string Title { get => "Magical Chest"; set { } }
        public string Description { get => "You come upon an inviting chest, brimming with promised treasure. Unfortunately, it seems to be held shut by magic, not by any lock you could pick."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new FollowUpEncounterChoice("Bash it open", "You hold your breath as one of your number closes her eyes and prepares to fight...", typeof(BashMagicalChestEncounter)));
                choices.Add(new FollowUpEncounterChoice("Dispel the lock", "The party holds still and silent as one of you quietly skulks off to surprise the creature...", typeof(DispelMagicalChestEncounter)));
                choices.Add(new MessageOnlyEncounterChoice("Leave it alone", "You decide to leave the chest alone for now."));
                return choices;
            }
            set { }
        }
    }
}
