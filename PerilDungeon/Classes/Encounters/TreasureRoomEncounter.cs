using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class TreasureRoomEncounter : IEncounter
    {
        public string Title { get => "Treasure Room"; set { } }
        public string Description { get => "Sure enough, the trap guarded a small chamber with treasure and valuable items!"; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new TreasureRoomEncounterChoice());
                return choices;
            }
            set { }
        }
    }
}
