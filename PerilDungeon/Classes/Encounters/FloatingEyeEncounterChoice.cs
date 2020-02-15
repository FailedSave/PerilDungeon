using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class FloatingEyeEncounterChoice : IEncounterChoice
    {
        public FloatingEyeEncounterChoice()
        {
            Text = $"Confront the floating eye";
        }

        public string Text { get; set; }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    foreach (Character c in party.PartyMembers)
                    {
                        c.AwardXP(15, party.Depth);
                        c.LoseMana(20 + EncounterSelector.rng.Next(party.Depth * 3));
                    }
                    party.Money += party.Depth * EncounterSelector.rng.Next(10, 20) + EncounterSelector.rng.Next(1, 5);
                    messages.Add("As you approach the strange eye, it looks at you balefully and you feel your mana leaking away. Before it can do anything with the stolen mana, you quickly dispatch the creature, and collect the valuable ichor it uses to levitate.");

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
