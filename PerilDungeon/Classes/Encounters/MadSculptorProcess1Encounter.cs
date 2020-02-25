using System;
using System.Collections.Generic;
using System.Text;

namespace PerilDungeon.Classes.Encounters
{
    class MadSculptorProcess1Encounter : IEncounter, ICustomEncounterImage
    {
        public MadSculptorProcess1Encounter(Character character)
        {
            Character = character;
        }
        public string Title { get => "Mad Sculptor"; set { } }
        public string Description { get => $"The old wizard grins with glee, and motions toward an empty pedestal in between two other statues. Obediently, {Character.YouOrNameVerbLower("strip", "strips")} off all of {Character.YourOrHerLower} clothes and {Character.Verb("step", "steps")} onto the pedestal. {Character.YouOrNameVerb("take", "takes")} a pose and {Character.Verb("hold", "holds")} {Character.YourOrHerLower} breath in anticipation..."; set { } }
        public Party Party { get; set; }
        public string ImageName { get => String.Format("madsculptor-{0}.png", Character.Name.ToLower()); set { } }
        public Character Character { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new MadSculptorProcess1EncounterChoice(Character));
                return choices;
            }
            set { }
        }

    }
}
