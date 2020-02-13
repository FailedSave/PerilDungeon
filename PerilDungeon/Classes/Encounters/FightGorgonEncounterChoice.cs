using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class FightGorgonEncounterChoice : IEncounterChoice
    {
        public FightGorgonEncounterChoice(Character character)
        {
            Character = character;
            Text = $"{Character.YouOrName}";
            IsAvailable = Character.CanAct;
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
                    Character.AwardXP(40, party.Depth);

                    if (Character.CheckSkill(Character.Combat, 13 + party.Depth * 2))
                    {
                        Character.LoseHealth(EncounterSelector.rng.Next(1, party.Depth));
                        messages.Add($"The gorgon fights fiercely, but {Character.YouOrNameLower} skillfully {(Character.IsPlayer ? "defeat" : "defeats")} her with only minor injuries.");
                        messages.Add($"The gorgon was carrying a pouch of semiprecious stones, which you loot from her now-harmless body.");
                        party.Money += party.Depth * EncounterSelector.rng.Next(20, 25) + EncounterSelector.rng.Next(1, 5);
                    }
                    else
                    {
                        Character.LoseHealth(30 + party.Depth * EncounterSelector.rng.Next(4, 8));
                        Character.AwardXP(10, party.Depth); //getting beaten up is good experience?
                        if (Character.Health > 0)
                        {
                            messages.Add($"The gorgon gets the better of the fight against {Character.YouOrNameLower}, although {Character.YouOrSheLower} finally {(Character.IsPlayer ? "wear" : "wears")} her down with dogged persistence.");
                            messages.Add($"The gorgon was carrying a pouch of semiprecious stones, which you loot from her now-harmless body.");
                            party.Money += party.Depth * EncounterSelector.rng.Next(20, 25) + EncounterSelector.rng.Next(1, 5);
                        }
                        else
                        {
                            if (Character.IsPlayer)
                            {
                                messages.Add("You fight your hardest, but blindfolded, you are no match for the gorgon. She defeats you and knocks you to the ground while your companions wait helplessly in the shadows. You are unable to resist as she pries your eyes open. As you gaze into her endless green eyes, you feel your body swiftly turn to stone. She chuckles and walks triumphantly away from your transformed body.");
                            }
                            else
                            {
                                messages.Add($"{Character.Name} fights valiantly, but blindfolded, she is no match for the gorgon. The creature defeats your friend and knocks her to the ground while your companions wait helplessly in the shadows. The gorgon pries {Character.PossessiveLower} eyes open and forces her to meet her gaze, quickly reducing her to a stone statue. She chuckles and leaves trimphantly.");
                            }
                            Character.AddStatus("Petrified");
                        }

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
