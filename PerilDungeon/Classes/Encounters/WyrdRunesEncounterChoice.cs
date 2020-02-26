using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class WyrdRunesEncounterChoice : IEncounterChoice
    {
        public WyrdRunesEncounterChoice(Character character)
        {
            Character = character;
            Text = $"Decipher the runes ({Character.YouOrName})";
            IsAvailable = character.CanAct;
        }
        public Character Character;

        public string Text { get; set; }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    double outcome = EncounterSelector.rng.NextDouble();
                    Character.AwardXP(35, party.Depth);
                    Character.MaxMana += 15;
                    Character.GainMana(30);
                    messages.Add($"As {Character.YouOrNameLower} {(Character.IsPlayer ? "read" : "reads")} the runes, they begin to glow, and energy pours out of them and suffuses {Character.YourOrHerLower} body.");
                    if (outcome < .4)
                    {
                        messages.Add($"The magical energy streams in without any end, beyond {Character.PossessiveLower} ability to handle it. {Character.Possessive} body glows brighter and brighter as ever-more energy pours in. When the light finally fades, {Character.YouAreOrNameIsLower} just a stone statue.");
                        Character.AddStatus(Status.Petrified);
                    }
                    else if (outcome < .7)
                    {
                        messages.Add($"The magical energy streams in without any end, beyond {Character.PossessiveLower} ability to handle it. {Character.Possessive} body glows brighter and brighter as ever-more energy pours in. Suddenly, the energy releases in a deafening explosion! {Character.YouAreOrNameIs} scarred and singed, but alive.");
                        Character.LoseHealth(25 + party.Depth * EncounterSelector.rng.Next(1, 5));

                    }
                    else
                    {
                        messages.Add($"The magical energy streams in furiously, but {Character.YouOrNameLower} {(Character.IsPlayer ? "manage" : "manages")} barely to control it. After the experience, {Character.YouOrNameLower} {(Character.IsPlayer ? "seem" : "seems")} to glow with newfound magical power and knowledge.");
                    }
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
