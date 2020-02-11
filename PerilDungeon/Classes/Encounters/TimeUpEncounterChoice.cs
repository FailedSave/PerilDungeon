using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class TimeUpEncounterChoice : IEncounterChoice
    {
        public string Text { get => "Despair"; set { } }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                return (Party party) =>
                {
                    List<string> messages = new List<string>();

                    messages.Add("Your party is overwhelmed by the perils surrounding every turn, and eventually finds themselves transformed into statues.");

                    foreach (Character c in party.PartyMembers)
                    {
                        c.AddStatus("Petrified");
                    }

                    return messages;
                };
            }
            set { }
        }

        public bool IsAvailable { get { return true; } set { } }

        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return EncounterSelector.PickEncounter(p, null);
        }
    }
}
