using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class FountainEncounterChoice : IEncounterChoice
    {
        public FountainEncounterChoice(Character character)
        {
            Character = character;
            Text = $"Drink the water ({Character.YouOrName})";
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
                    Character.AwardXP(10, party.Depth);
                    double outcome = EncounterSelector.rng.NextDouble();
                    if (outcome < .05)
                    {
                        messages.Add($"The fountain's magical healing properties are potent, making {Character.YouOrNameLower} permanently stronger!");
                        Character.MaxHealth += 5;
                        Character.GainHealth(EncounterSelector.rng.Next(5, 20) + party.Depth * 2);
                    }
                    else if (outcome < .4)
                    {
                        messages.Add($"The fountain's water invigorates {Character.PossessiveLower} tired body.");
                        Character.GainHealth(15 + EncounterSelector.rng.Next(10, 25) + party.Depth * 2);
                    }
                    else if (outcome < .75)
                    {
                        messages.Add($"The fountain's water energizes {Character.PossessiveLower} weary mind.");
                        Character.GainMana(15 + EncounterSelector.rng.Next(5, 20) + party.Depth * 2);
                    }
                    else if (outcome < .95)
                    {
                        messages.Add($"The spring's water is cool and refreshing. However, a few seconds after draining the basin, {Character.YouOrNameLower} {(Character.IsPlayer ? "realize" : "realizes")} somthing is terribly wrong. {Character.YouAreOrNameIs} filled with an overwhelming sense of dread, but it doesn't last long; in a few heartbeats more, {Character.YouAreOrNameIsLower} has been turned to stone by the magic.");
                        Character.GainHealth(EncounterSelector.rng.Next(10, 25) + party.Depth * 2);
                        Character.AddStatus(Status.Petrified);
                    }
                    else
                    {
                        messages.Add($"As {Character.YouOrNameLower} dips {Character.YourOrHerLower} hands into the spring to drink, {Character.YouOrSheLower} {(Character.IsPlayer ? "jerk" : "jerks")} them back quickly; this is no water, but corrosive acid! Unfortunately {Character.PossessiveLower} clothes are destroyed from carelessly splashed liquid, although {Character.YouAreOrNameIsLower} not seriously hurt.");
                        Character.LoseHealth(1);
                        Character.DestroyBodyItem();
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
