using System;
using System.Collections.Generic;
using System.Text;

namespace PerilDungeon.Classes.Encounters
{
    class MadSculptorProcess1EncounterChoice : IEncounterChoice
    {
        public MadSculptorProcess1EncounterChoice(Character character)
        {
            Character = character;
        }
        public Character Character;

        public string Text { get => "Wait Patiently"; set { } }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    Character.AwardXP(20, party.Depth);
                    Character.AddStatus(Status.Petrified);
                    Character.PortraitOverride = PortraitOverride.MadSculptor;
                    Character.DestroyBodyItem();
                    messages.Add($"The old man casts a spell with the air of someone who's practiced many times. In a matter of seconds, {Character.YouOrNameVerbLower("become", "becomes")} just another of the many nude statues in his gallery.");
                    return messages;
                };
            }
            set { }
        }
        public bool IsAvailable { get => true; set { } }

        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return new MadSculptorProcess2Encounter(Character);
        }
    }
}
