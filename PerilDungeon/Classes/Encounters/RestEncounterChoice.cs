using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class RestEncounterChoice : IEncounterChoice
    {
        public string Text { get => "Rest"; set { } }
        public Func<Party, Character, IEnumerable<string>> Choose
        {
            get
            {
                return (Party party, Character character) =>
                {
                    party.TimeRemaining -= 10;
                    foreach(Character c in party.PartyMembers)
                    {
                        c.Rest();
                    }
                    List<string> messages = new List<string>();
                    messages.Add("You and your friends take a short rest to catch your breath and tend to your scrapes.");
                    return messages;
                };
            }
            set { }
        }

        public bool IsAvailable { get { return true; } set { } }

        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return new ExplorationEncounter();
        }

    }
}
