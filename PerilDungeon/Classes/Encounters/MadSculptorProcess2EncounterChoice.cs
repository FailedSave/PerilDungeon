using System;
using System.Collections.Generic;
using System.Text;

namespace PerilDungeon.Classes.Encounters
{
    class MadSculptorProcess2EncounterChoice : IEncounterChoice
    {
        public MadSculptorProcess2EncounterChoice(Character character)
        {
            Character = character;
        }
        public Character Character;

        public string Text { get => "Collect"; set { } }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    party.Money += party.Depth * EncounterSelector.rng.Next(40, 50) + EncounterSelector.rng.Next(1, 5);
                    messages.Add($"Your party collects the payment, hoping that {Character.PossessiveLower} sacrifice helps you all earn your freedom in the end.");
                    return messages;
                };
            }
            set { }
        }
        public bool IsAvailable { get => true; set { } }

        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return EncounterSelector.PickEncounter(p, typeof(ExplorationEncounter));
        }
    }
}
