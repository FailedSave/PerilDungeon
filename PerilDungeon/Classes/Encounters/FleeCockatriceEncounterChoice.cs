using System;
using System.Collections.Generic;
using System.Text;

namespace PerilDungeon.Classes.Encounters
{
    class FleeCockatriceEncounterChoice : IEncounterChoice
    {
        public FleeCockatriceEncounterChoice()
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
                    if (EncounterSelector.rng.NextDouble() < .9)
                    {
                        messages.Add($"You easily manage to outpace the clumsy, slow-flying monsters");
                    }
                    else
                    {
                        Character target = party.GetRandomActiveCharacter();
                        target.AwardXP(10, party.Depth);
                        target.AddStatus("Petrified");
                        messages.Add($"Although you all flee quickly, {target.YouOrNameLower} {(target.IsPlayer ? "stumble" : "stumbles")} on an unseen obstacle as you escape. The creatures manage to catch up and quickly petrify {target.YouOrName} with their deadly beaks, leaving {target.YourOrHerLower} a helpless statue.");
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
