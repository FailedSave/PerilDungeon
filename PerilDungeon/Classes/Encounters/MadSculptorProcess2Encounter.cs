using System;
using System.Collections.Generic;
using System.Text;

namespace PerilDungeon.Classes.Encounters
{
    class MadSculptorProcess2Encounter : IEncounter, ICustomEncounterImage
    {
        public MadSculptorProcess2Encounter(Character character)
        {
            Character = character;
        }
        public string Title { get => "Mad Sculptor"; set { } }
        public string Description { get => $"{Character.YouOrNameVerb("stand", "stands")} as a silent statue in the old wizard's gallery. True to his promise, he hands over a large pouch of coins. A servitor imp sketches {Character.PossessiveLower} nude stone body in a thick parchment codex with startling speed. When the creature is finished, the weird old man sighs mournfully and gestures you on your way."; set { } }
        public Party Party { get; set; }
        public string ImageName { get => String.Format("madsculptor-{0}-stone.png", Character.Name.ToLower()); set { } }
        public Character Character { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new MadSculptorProcess2EncounterChoice(Character));
                return choices;
            }
            set { }
        }
    }
}
