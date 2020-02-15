using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class BetrayalAltarEncounter : IEncounter
    {
        public string Title { get => "Altar of Jushalan"; set { } }
        public string Description { get => "You come across an old, forgotten altar covered in masks. You recognize it as an altar of the outlawed god Jushalan, a malicious deity of tricks and betrayal. Despite its age, you sense Jushalan's presence. The smirking god offers you wealth and power if you are willing to betray your friends. Nervously, you look around--they seem to have been made the same offer."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new BetrayalEncounterChoice(c));
                }
                choices.Add(new MessageOnlyEncounterChoice("Leave", "Nervously looking around, you hurry away before any of you succumb to the tempting divine whispers in your mind."));
                return choices;
            }
            set { }
        }
    }
}
