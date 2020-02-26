using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class EscapeEncounterChoice : IEncounterChoice
    {
        public EscapeEncounterChoice(Party party)
        {
            IsAvailable = party.MainQuestProgress == MainQuestProgress.GotShard;
        }

        public StairsDirection Direction { get; set; }

        public string Text { get => "Escape!"; set { } }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();

                return (Party party) =>
                {
                    messages.Add("Brandishing the Suncrystal shard proudly, you fling open the dungeon door and step outside.");
                    foreach (Character c in party.PartyMembers)
                    {
                        c.RemoveStatus(Status.Petrified);
                        c.BodyItem = new Items.ClothesEquipment();
                    }
                    party.MainQuestProgress = MainQuestProgress.Victory;
                    return messages;
                };
            }
            set { }
        }
        public bool IsAvailable { get; set; }

        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return EncounterSelector.PickEncounter(p, null);
        }

    }
}
