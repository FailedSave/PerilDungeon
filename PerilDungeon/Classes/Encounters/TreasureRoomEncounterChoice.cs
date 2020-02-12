using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class TreasureRoomEncounterChoice : IEncounterChoice
    {
        public string Text { get => "Score!"; set { } }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                return (Party party) =>
                {
                    List<string> messages = new List<string>();

                    messages.Add("You add the loot to your bags and move on.");

                    party.Money += party.Depth * EncounterSelector.rng.Next(20, 40) + EncounterSelector.rng.Next(1, 5);

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
