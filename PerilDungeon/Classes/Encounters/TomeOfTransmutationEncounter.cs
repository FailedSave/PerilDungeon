using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class TomeOfTransmutationEncounter : IEncounter
    {
        public string Title { get => "Tome of Transfiguration"; set { } }
        public string Description { get => "On the statue of an unlucky sorcerer, you find an undamaged backpack. Most of the supplies are of little use, but there is one exception: an unusual book. Obviously magical, the pattern of the leather-bound cover seems to shift and morph, changing its color, texture, and even smell from one moment for the next. On one hand, some additional magical knowledge might be useful, but on the other hand, it didn't seem to help its previous owner..."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new TomeOfTransmutationEncounterChoice(c));
                }
                choices.Add(new MessageOnlyEncounterChoice("Ignore it", "This book looks like transformative trouble. You decide to leave it for someone more desperate."));
                return choices;
            }
            set { }
        }
    }
}
