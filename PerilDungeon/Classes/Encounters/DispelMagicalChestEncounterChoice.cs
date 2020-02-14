using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class DispelMagicalChestEncounterChoice : IEncounterChoice
    {
        public DispelMagicalChestEncounterChoice(Character character)
        {
            Character = character;
            Text = $"{Character.YouOrName}";
            IsAvailable = Character.CanAct && Character.Mana > 5;
        }
        public Character Character;

        private const int ManaRequired = 60;

        public string Text { get; set; }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    Character.AwardXP(20, party.Depth);
                    bool success = (EncounterSelector.rng.Next(ManaRequired) + Character.Mana > ManaRequired);
                    Character.LoseMana(ManaRequired);

                    if (success)
                    {
                        messages.Add($"{Character.YouOrName} {(Character.IsPlayer ? "concentrate and chant" : "concentrates and chants")}, and the chest glows a pale violet and silently opens with a click. The coins and gems inside are yours.");
                        party.Money += party.Depth * EncounterSelector.rng.Next(15, 25) + EncounterSelector.rng.Next(1, 5);
                    }
                    else
                    {
                        messages.Add($"{Character.YouOrName} {(Character.IsPlayer ? "concentrate and chant" : "concentrates and chants")}, but {Character.YouOrSheLower} loses {Character.YourOrHerLower} concentration, and the spell fails. {Character.YouAreOrNameIs} mentally drained, and the chest remains sealed.");
                    }

                    //else, it's safe
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
