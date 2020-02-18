using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class SunstoneShardEncounter : IEncounter
    {
        public string Title { get => "Sunstone Chamber"; set { } }
        public string Description { get => "Impossibly, a ray of light from the surface reaches this chamber, hundreds of feet beneath the surface. It strikes a translucent golden gem, bathing the entire room in a divine radiance. Despite your misgivings about the god, you are filled with awe at the beautiful sight. This is your chance to claim a piece of Sunstone as your token of Kylan's approval."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new SunstoneClaimEncounterChoice());
                choices.Add(new MessageOnlyEncounterChoice("Leave", "The Sunstone is beautiful, but you know you are not yet ready to touch the divine."));
                return choices;
            }
            set { }
        }
    }
}
