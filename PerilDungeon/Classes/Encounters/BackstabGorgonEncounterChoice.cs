using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class BackstabGorgonEncounterChoice : IEncounterChoice
    {
        public BackstabGorgonEncounterChoice(Character character)
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
                    Character.AwardXP(30, party.Depth);

                    if (Character.CheckSkill(Character.Thievery, 14 + party.Depth * 2))
                    {
                        messages.Add($"{Character.YouAreOrNameIs} able to ambush the gorgon and put a dagger into her back before she notices. She drops harmlessly face-first to the ground.");
                        messages.Add($"The gorgon was carrying a pouch of semiprecious stones, which you loot from her now-harmless body.");
                        party.Money += party.Depth * EncounterSelector.rng.Next(20, 25) + EncounterSelector.rng.Next(1, 5);
                    }
                    else
                    {
                        if (Character.IsPlayer)
                        {
                            messages.Add("You sneak up on the gorgon and prepare to attack, but just as you jump out, she turns around and meets you with her gaze. You freeze instantly in place as your flesh begins transforming to stone. In a heartbeat, you are a statue, and no longer a threat to her.");
                        }
                        else
                        {
                            messages.Add($"{Character.Name} sneaks up on the gorgon and prepares to attack, but just as she jumps out, the gorgon turns around and meets {Character.Name} with her deadly gaze. She freeze instantly in place as her flesh begins transforming to stone. In a heartbeat, {Character.Name} is a statue, and no longer a threat to the gorgon.");
                        }
                        Character.AddStatus("Petrified");
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
