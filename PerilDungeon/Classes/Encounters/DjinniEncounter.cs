using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class DjinniEncounter : IEncounter
    {
        public string Title { get => "Gracious Djinni"; set { } }
        public string Description { get => "While sorting through your treasures, you find an old ring with an unusual glyph etched on it. As you rub the dirt off, an ethereal humanoid emerges—a djinni! These conniving spirits grant power—always at a price. This one grins and offers to grant one of you the physical and magical might you'll need to make it through the next few rooms..."; set { } }
        public Party Party { get; set; }
        public string ImageName { get => "madsculptor-empty-pedestal.png"; set { } }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new AcceptDjinniEncounterChoice(c));
                }
                choices.Add(new MessageOnlyEncounterChoice("Decline", "You've all heard stories about the terrible prices djinnis extract, so you decline. The djinni nods curtly and leaves straight through a wall, never to be seen again."));
                return choices;
            }
            set { }
        }
    }
}
