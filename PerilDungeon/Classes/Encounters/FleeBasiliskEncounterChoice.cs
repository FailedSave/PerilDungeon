using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class FleeBasiliskEncounterChoice : IEncounterChoice
    {
        public FleeBasiliskEncounterChoice()
        {
            Text = $"Flee";
        }

        public string Text { get; set; }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    //3/4 chance of escaping
                    if (EncounterSelector.rng.NextDouble() < .75)
                    {
                        messages.Add($"You manage to escape from the slow-moving lizard.");
                    }
                    else
                    {
                        Character target = party.GetRandomActiveCharacter();
                        target.AwardXP(10, party.Depth);
                        if (EncounterSelector.rng.NextDouble() < 0.4)
                        {   //just an injury
                            messages.Add($"You manage to escape, but not before the monster takes a bite from {target.PossessiveLower} leg.");
                            target.LoseHealth(party.Depth * 2 + EncounterSelector.rng.Next(1, 5));
                        }
                        else
                        {
                            target.AddStatus(Status.Petrified);
                            messages.Add($"Before you can turn to flee, {target.YouOrNameLower} {(target.IsPlayer ? "meet" : "meets")} the beast's gaze. It scuttles off, leaving {target.YourOrHerLower} a helpless statue.");
                        }
                    }

                    return messages;
                };
            }
            set { }
        }
        public bool IsAvailable { get { return true; } set { } }

        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return EncounterSelector.PickEncounter(p, typeof(ExplorationEncounter));
        }

    }
}
