using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class TrappedDoorEncounterChoice : IEncounterChoice
    {
        public TrappedDoorEncounterChoice(Character character)
        {
            Character = character;
            Text = $"Open it carefully ({Character.YouOrName})";
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
                    Character.AwardXP(35, party.Depth);
                    if (Character.CheckSkill(Character.Thievery, 8 + party.Depth * 2))
                    {
                        messages.Add($"{Character.YouAreOrNameIs} able to disable the trap mechanism, defeat the lock, and the door swings open with a satisfying heft.");
                    }
                    else
                    {
                        if (Character.IsPlayer)
                        {
                            messages.Add("You reach for the lock, but don't see the concealed aperture until you hear the tiny whir of the trap activating. You instinctively raise your hands to protect yourself, but the tiny needle flies out and pierces your skin.");
                        }
                        else
                        {
                            messages.Add($"{Character.Name} reaches for the lock, but doesn't see the concealed aperture until she hears the tiny whir of the trap activating. She tries to dodge out of the way, but a tiny needle flies out and pierces her skin.");
                        }
                        if (Character.HealthRatio < EncounterSelector.rng.NextDouble() + 0.1)
                        {   //failed the stamina check, transformed
                            Character.LoseHealth(1);
                            if (Character.IsPlayer)
                            {
                                messages.Add("It barely even hurts, but the petrifying poison spreads through your system on your rushing blood, transforming you to stone where you stand.");
                            }
                            else
                            {
                                messages.Add($"{Character.Name} yelps briefly, but is quickly silenced as the petrifying poison courses through her body, transforming her to stone where she stands.");
                            }
                            Character.AddStatus("Petrified");
                        }
                        else
                        {
                            Character.LoseHealth(5 * party.Depth + EncounterSelector.rng.Next(1, 5));
                            if (Character.IsPlayer)
                            {
                                messages.Add("You feel the eldritch poison burn within you, but you grit your teeth and manage to shake the effects off.");
                            }
                            else
                            {
                                messages.Add($"{Character.Name} grits her teeth and manages to shake off the transformative effects of the poison.");
                            }
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
            return EncounterSelector.PickEncounter(p, typeof(TreasureRoomEncounter));
        }
    }
}
