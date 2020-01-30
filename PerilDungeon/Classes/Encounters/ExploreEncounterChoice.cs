using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class ExploreEncounterChoice : IEncounterChoice
    {
        public ExploreEncounterChoice(string text, bool includeRest)
        {
            Text = text;
            IncludeRest = includeRest;
        }

        public bool IncludeRest { get; set; }

        public string Text { get; set; }
        public Func<Party, Character, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();

                return (Party party, Character character) =>
                {
                    if (IncludeRest)
                    {
                        party.TimeRemaining -= 10;
                        foreach (Character c in party.PartyMembers)
                        {
                            c.Rest();
                        }
                        messages.Add("You wind your way carefully through the twisting passages...");
                    }
                    else
                    {
                        party.TimeRemaining -= 5;
                        messages.Add("You stride boldly down the twisting, dark passages...");
                    }
                    return messages;
                };
            }
            set { }
        }
        public bool IsAvailable { get => true; set { } }

        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return EncounterSelector.PickEncounter(p, null);
        }
    }
}
