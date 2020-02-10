using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class FleeUnweaverEncounterChoice : IEncounterChoice
    {
        public FleeUnweaverEncounterChoice()
        {
            Text = $"Flee from the unweaver";
        }

        public string Text { get; set; }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    if (EncounterSelector.rng.NextDouble() < .5)
                    {
                        messages.Add($"You manage to escape from the strange crustacean.");
                    }
                    else
                    {
                        Character target = party.GetRandomCharacter();
                        if (target.CanAct)
                        {
                            target.AwardXP(10, party.Depth);
                            if (target.BodyItem == null)
                            {
                                messages.Add($"The unweaver manages to catch up to {target.YouOrNameLower}, but its antennae slide harmlessly over {target.YourOrHerLower} bare skin. You escape without injury.");
                            }
                            else
                            {
                                target.DestroyBodyItem();
                                messages.Add($"The unweaver manages to catch up to {target.YouOrNameLower}, and swiftly destroys {target.YourOrHerLower} clothes with its antennae. You make your escape while it enjoys its meal.");
                            }
                        }
                        else
                        {
                            if (target.BodyItem == null)
                            {
                                messages.Add($"The unweaver approaches the statue of {target.YouOrNameLower}, but soon wanders away in disinterest.");
                            }
                            else
                            {
                                target.DestroyBodyItem();
                                messages.Add($"The unweaver approaches the statue of {target.YouOrNameLower} and devours its clothes. The rest of the party manages to conceal themselves while it finishes its meal.");
                            }

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
