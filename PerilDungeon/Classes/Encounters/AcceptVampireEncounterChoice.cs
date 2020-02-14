using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class AcceptVampireEncounterChoice : IEncounterChoice
    {
        public AcceptVampireEncounterChoice(Character character)
        {
            BloodAmount = 20 + 5 * character.Level;
            Character = character;
            Text = $"Accept ({Character.YouOrName})";
            IsAvailable = Character.CanAct && Character.Health >= BloodAmount;
        }
        public Character Character;

        private int BloodAmount;

        public string Text { get; set; }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    party.Money += BloodAmount * 2;
                    Character.LoseHealth(BloodAmount);
                    Character.GainMana(BloodAmount / 10);
                    Character.AwardXP(10, party.Depth);
                    messages.Add($"The vampire smiles gratefully, then bares her fangs and takes a long drink from {Character.PossessiveLower} offered wrist. When she's finished, she licks the wound clean and hands over a bag of coins as promised. {Character.YouAreOrNameIs} drained by the experience, but it's surpisingly pleasant.");
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
