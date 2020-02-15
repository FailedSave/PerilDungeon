using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class FloatingEyeEncounter : IEncounter
    {
        public string Title { get => "Floating Eye"; set { } }
        public string Description { get => "Floating off down the corridor, you see a small creature hovering in midair; a dispeller eye. While not a threat in combat, the creature will probably drain your mana reserves if you approach it. The only other way is down a hallway full of the deadly miasma that pervades the dungeon."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new FloatingEyeEncounterChoice());
                choices.Add(new BadAirEncounterChoice("Avoid the creature"));
                return choices;
            }
            set { }
        }
    }
}
