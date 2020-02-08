using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class FightUnweaverEncounterChoice : IEncounterChoice
    {
        public FightUnweaverEncounterChoice()
        {
            Text = $"Confront the unweaver";
        }

        public string Text { get; set; }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    Character target = party.GetRandomActiveCharacter();
                    target.AwardXP(20, party.Depth);
                    party.Money += party.Depth * EncounterSelector.rng.Next(10, 20) + EncounterSelector.rng.Next(1, 5);

                    if (target.BodyItem == null)
                    {
                        messages.Add($"{target.YouOrName} {(target.IsPlayer ? "engage" : "engages")} the unweaver boldly. The unweaver's antennae slide harmlessly off living flesh, and the creature is quickly defeated.");
                    }
                    else
                    {
                        if (target.CheckSkill(target.Combat, 10 + party.Depth * 2) && EncounterSelector.rng.NextDouble() > .5)
                        {
                            messages.Add($"{target.YouOrName} {(target.IsPlayer ? "engage" : "engages")} the unweaver boldly. {target.YouAreOrNameIs} able to defeat the creature while staying away from its antennae, and harvest them as a trophy.");
                            party.Money += party.Depth * EncounterSelector.rng.Next(10, 20) + EncounterSelector.rng.Next(1, 5);
                        }
                        else
                        {
                            target.BodyItem = null;
                            messages.Add($"As {target.YouOrNameLower} {(target.IsPlayer ? "fight" : "fights")} the unweaver, it manages to rake {target.YourOrHerLower} clothes with its antennae. It squeals in joy, as they turn to dust and threads and fall away, leaving {target.YouOrHerLower} naked. The monster is defeated, but {target.PossessiveLower} clothes are gone forever.");
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
