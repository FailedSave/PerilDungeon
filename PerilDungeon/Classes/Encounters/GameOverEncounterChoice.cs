using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class GameOverEncounterChoice : IEncounterChoice
    {
        public string Text { get => "The End"; set { } }
        public Func<Party, Character, IEnumerable<string>> Choose
        {
            get
            {
                return (Party party, Character character) =>
                {
                    List<string> messages = new List<string>();

                    messages.Add("GAME OVER");
                    party.GameOver = true;
                    return messages;
                };
            }
            set { }
        }

        public bool IsAvailable { get { return true; } set { } }

        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return EncounterSelector.PickEncounter(p, typeof(FirstEncounter));
        }
    }
}
