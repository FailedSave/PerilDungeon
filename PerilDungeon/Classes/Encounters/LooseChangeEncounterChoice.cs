using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class LooseChangeEncounterChoice : IEncounterChoice
    {
        public LooseChangeEncounterChoice()
        {
            Text = $"Take the coin purse";
        }

        public string Text { get; set; }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    party.Money += party.Depth * EncounterSelector.rng.Next(1, 5) + EncounterSelector.rng.Next(1, 5);
                    messages.Add("You help yourself to the coin purse while silently hoping you manage to avoid this poor soul's fate.");

                    return messages;
                };
            }
            set { }
        }
        public bool IsAvailable { get { return true; } set { } }

        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return EncounterSelector.PickEncounter(p, typeof(ExplorationEncounter));
        }
    }
}
