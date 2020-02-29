using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class AcceptSculptorEncounterChoice : IEncounterChoice
    {
        public AcceptSculptorEncounterChoice(Character character)
        {
            Character = character;
            Text = $"Accept ({Character.YouOrName})";
            IsAvailable = Character.CanAct && !Character.HasStatus(Status.Polymorphed);
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
                    messages.Add($"{Character.YouOrName} {(Character.IsPlayer ? "nod" : "nods")} in cautious agreement...");
                    return messages;
                };
            }
            set { }
        }
        public bool IsAvailable { get; set; }

        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return new MadSculptorProcess1Encounter(Character);
        }
    }
}
