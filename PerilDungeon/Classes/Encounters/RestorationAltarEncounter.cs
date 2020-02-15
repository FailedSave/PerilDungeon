using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class RestorationAltarEncounter : IEncounter
    {
        public string Title { get => "Altar of Olanna"; set { } }
        public string Description { get => "Hidden away in a corner, concealed by detritus and almost unnoticeable, you find a makeshift altar to Olanna: goddess of life and rival of Kylan. As you approach it, you feel Olanna's presence offering you a faint thread of hope in this dark place."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new RestorationAltarEncounterChoice(c));
                }
                return choices;
            }
            set { }
        }
    }
}
