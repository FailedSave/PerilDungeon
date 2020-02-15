using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class RestorationAltarEncounterChoice : IEncounterChoice
    {
        public RestorationAltarEncounterChoice(Character character)
        {
            Character = character;
            if (Character.CanAct)
            {
                Text = $"Pray ({Character.YouOrName})";
            }
            else
            {
                Text = $"Pray for {Character.Name}";
            }
            IsAvailable = true;
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
                    if (!Character.CanAct)
                    {
                        Character.RemoveStatus("Petrified");
                        if (Character.IsPlayer)
                        {
                            messages.Add($"Deep within your petrified soul, you feel the restorative touch of the goddess Olanna. She has heard the prayers of your friends, and she returns you to life with her blessing.");
                        }
                        else
                        {
                            messages.Add($"The goddess Olanna hears the prayer, and kindly restores {Character.Name} to life. Gasping for air with renewed lungs, she flexes her newly-restored limbs and smiles in her newfound freedom.");
                        }
                    }
                    else //restore HP
                    {
                        Character.MaxHealth += 5;
                        Character.GainHealth(30 + EncounterSelector.rng.Next(20));
                        {
                            messages.Add($"Olanna grants her blessing to {Character.YouOrNameLower}, filling {Character.YouOrHerLower} with renewed health and vigor.");
                        }
                    }

                    Character.AwardXP(5, party.Depth);
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
