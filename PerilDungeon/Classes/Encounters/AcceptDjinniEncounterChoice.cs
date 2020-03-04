using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class AcceptDjinniEncounterChoice : IEncounterChoice
    {
        public AcceptDjinniEncounterChoice(Character character)
        {
            Character = character;
            Text = $"Accept ({Character.YouOrName})";
            IsAvailable = Character.CanAct && !Character.HasStatus(Status.Empowered);
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
                    party.EncountersSinceDjinn = 0;
                    messages.Add($"{Character.YouOrNameVerb("readily agree", "readily agrees")} to the djinni's offer. The djinni raises her arms and thunder clashes dramatically, and {Character.YouOrNameVerbLower("feel", "feels")} a mighty rush of power. {Character.YouAreOrNameIs} stronger, faster, and brimming with magic. There is also a strange metal armband bound around {Character.PossessiveLower} wrist. \"A reminder of the favor you owe,\" the djinni smirks, before melting back into the ring.");
                    Character.AddStatus(Status.Empowered);
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
