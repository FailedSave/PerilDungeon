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
                    party.Money += party.Depth * EncounterSelector.rng.Next(30, 40) + EncounterSelector.rng.Next(1, 5);
                    Character.AwardXP(10, party.Depth);
                    Character.AddStatus("Petrified");
                    Character.DestroyBodyItem();
                    messages.Add($"The old man smiles happily and procures a large sack of coins, which you add to the party supplies. He then smiles and points to an empty pedestal. Obediently, {Character.YouOrNameLower} {(Character.IsPlayer ? "strip" : "strips")} naked and steps up onto it. The old man casts a spell with the air of someone who's practiced many times. In a matter of seconds, {(Character.IsPlayer ? "you become" : "she becomes")} just another of the many nude statues in his gallery.");
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
