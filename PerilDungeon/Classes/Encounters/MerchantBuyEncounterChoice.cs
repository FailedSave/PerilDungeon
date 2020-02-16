using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class MerchantBuyEncounterChoice : IEncounterChoice
    {
        public MerchantBuyEncounterChoice(Character character, Party party, int cost, Action<Character> effect, string message)
        {
            Character = character;
            Text = $"Purchase for {Character.YouOrNameLower}";
            Cost = cost;
            Effect = effect;
            IsAvailable = Character.CanAct && party.Money >= Cost;
            Message = message;
        }
        public Character Character;

        private int Cost;

        private Action<Character> Effect;

        private string Message;

        public string Text { get; set; }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    party.Money -= Cost;
                    Effect(Character);

                    messages.Add(string.Format(Message, Character.YouOrNameLower, Character.PossessiveLower));
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
