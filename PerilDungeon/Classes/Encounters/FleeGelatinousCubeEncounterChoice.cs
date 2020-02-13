using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class FleeGelatinousCubeEncounterChoice : IEncounterChoice
    {
        public FleeGelatinousCubeEncounterChoice()
        {
            Text = $"Run away";
        }

        public string Text { get; set; }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    party.TimeRemaining -= 10;
                    foreach (Character c in party.PartyMembers)
                    {
                        c.LoseHealth(5 + EncounterSelector.rng.Next(1, party.Depth * 2));
                    }

                    messages.Add("You turn tail and sprint away. The run takes a lot out of you, but you leave the jelly behind and manage to catch your breath some distance away.");

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
