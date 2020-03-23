using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class TomeOfTransmutationEncounterChoice : IEncounterChoice
    {
        public TomeOfTransmutationEncounterChoice(Character character)
        {
            Character = character;
            Text = $"Study it ({Character.YouOrName})";
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
                    Character.AwardXP(20, party.Depth);
                    Character.MaxMana += 10;
                    Character.AddStatus(Status.Inspired);
                    messages.Add($"{Character.YouOrNameVerb("begin", "begins")} to read the strange book. It's remarkably easy to read; each page takes only a few seconds to read as the knowledge seems to simply flow into {Character.YourOrHerLower} mind. The shifting tome indeed focuses on transformation magic, laying out both how to most efficiently perform it and pointers for when to use it for the most dramatic effects. {Character.YouAreOrNameIsLower} left with a lasting feeling of inspiration.");

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
