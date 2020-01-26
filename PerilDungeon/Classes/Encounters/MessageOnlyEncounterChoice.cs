using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    //An Encounter choice that only prints a message and doesn't affect the party.
    public class MessageOnlyEncounterChoice : IEncounterChoice
    {
        public MessageOnlyEncounterChoice(string text, string message)
        {
            Text = text;
            Message = message;
        }

        public string Text { get; set; }
        public string Message { get; set; }
        public Func<Party, Character, IEnumerable<string>> Choose
        {
            get
            {
                return (Party party, Character character) =>
              {
                  List<string> messages = new List<string>();
                  messages.Add(Message);
                  return messages;
              };
            }
            set { }
        }

        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return new BasicEncounter();
        }

    }
}
