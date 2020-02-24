using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class MadSculptorEncounter : IEncounter, ICustomEncounterImage
    {
        public string Title { get => "Mad Sculptor"; set { } }
        public string Description { get => "Deep within the delve, you come upon a gallery--dozens of statues of former adventurers, all in one place. As you wander around looking for valuables, an old wizard approaches you. He introduces himself as an eccentric artist who is quite taken by the appearance of your crew. He makes you an unusual offer: for a large sum of money, you'll permit him to transform one of your party into a nude statue. You'll be allowed to turn her back after he makes some sketches."; set { } }
        public Party Party { get; set; }
        public string ImageName { get => "madsculptor-empty-pedestal.png"; set { } }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new AcceptSculptorEncounterChoice(c));
                }
                choices.Add(new MessageOnlyEncounterChoice("Decline", "You decline the weird old man's offer. As you leave, he taunts you. \"I'll have you in my gallery soon enough anyway!\" You grimace and move on."));
                return choices;
            }
            set { }
        }
    }
}
