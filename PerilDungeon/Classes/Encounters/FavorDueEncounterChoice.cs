using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class FavorDueEncounterChoice : IEncounterChoice
    {
        public FavorDueEncounterChoice()
        {
        }

        public string Text { get => "\"Stop, wait...\""; set { } }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    List<Character> empowered = party.GetCharactersWithStatus(Status.Empowered);
                    Character target = empowered[EncounterSelector.rng.Next(0, empowered.Count)];
                    target.RemoveStatus(Status.Empowered);
                    target.LoseMana(1000);
                    target.AddStatus(Status.Petrified);

                    messages.Add($"{target.YouOrNameVerb("try", "tries")} to fight it, but the djinni's compulsion is irresistable. {target.YouOrNameVerb("summon", "summons")} all of the djinni's power and {(target.IsPlayer ? "channel" : "channels")} it into a mighty transformation spell—directed straight at {(target.IsPlayer ? "yourself" : "herself")}. The djinni's signature crashing of thunder sounds out, and the spell finishes; {target.YouOrNameVerbLower("have turned yourself", "has turned herself")} into a peaceful, silent statue. The djinni snickers as you all stand on helplessly, then vanishes forever.");

                    return messages;
                };
            }
            set { }
        }
        public bool IsAvailable { get => true; set { } }

        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return EncounterSelector.PickEncounter(p, typeof(ExplorationEncounter));
        }
    }
}
