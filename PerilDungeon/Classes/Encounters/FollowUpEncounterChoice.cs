using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class FollowUpEncounterChoice : IEncounterChoice
    {
        public FollowUpEncounterChoice(string text, string message, Type nextEncounter)
        {
            Text = text;
            Message = message;
            NextEncounter = nextEncounter;
        }

        public string Text { get; set; }
        public string Message { get; set; }
        public Type NextEncounter { get; set; }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                return (Party party) =>
                {
                    List<string> messages = new List<string>();
                    messages.Add(Message);
                    return messages;
                };
            }
            set { }
        }

        public bool IsAvailable { get { return true; } set { } }

        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return EncounterSelector.PickEncounter(p, NextEncounter);
        }
    }
}
