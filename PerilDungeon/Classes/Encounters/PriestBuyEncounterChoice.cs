using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters 
{
    public class PriestBuyEncounterChoice : IEncounterChoice
    {
        public PriestBuyEncounterChoice(Party party)
        {
            Text = $"Purchase the book";
            Cost = 2500;
            IsAvailable = party.Money >= Cost;
        }

        private int Cost;

        public string Text { get; set; }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    party.Money -= Cost;
                    party.MainQuestProgress = MainQuestProgress.GotBook;

                    messages.Add("Sighing, you exchange a heavy sack of coins and gems for a small, worn book. The book, written by a centuries-old saint, descibes specific prayers that must be recited while claiming a Suncrystal shard to pick it up without incurring Kylan's wrath. You keep it with you so you'll be able to remember it when the time comes.");
                    return messages;
                };
            }
            set { }
        }
        public bool IsAvailable { get; set; }

        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return EncounterSelector.PickEncounter(p, typeof(ExplorationEncounter));
        }
    }
}
