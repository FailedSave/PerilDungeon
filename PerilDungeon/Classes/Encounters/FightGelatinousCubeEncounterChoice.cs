using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class FightGelatinousCubeEncounterChoice : IEncounterChoice
    {
        public FightGelatinousCubeEncounterChoice()
        {
            Text = $"Stab the jelly";
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
                        if (c.CanAct)
                        {
                            c.AwardXP(15, party.Depth);
                            c.LoseHealth(EncounterSelector.rng.Next(1, party.Depth * 2));
                        }
                    }

                    Character target = party.GetRandomCharacter();

                    if (!target.CanAct && target.BodyItem != null)
                    {
                        target.DestroyBodyItem();
                        messages.Add($"The creature's acid dissoves the clothing from the statue of {target.YouOrNameLower}, leaving the stone body clean and unharmed.");
                    }
                    else if (target.CanAct && target.BodyItem != null && EncounterSelector.rng.NextDouble() < .5)
                    {
                        target.DestroyBodyItem();
                        messages.Add($"The creature's acid dissoves the clothing from {target.PossessiveLower} body. {target.YouOrShe} cringes and shivers, but {target.PossessiveLower} flesh is only slightly burned.");
                    }

                    messages.Add("The creature's acid, adapted to quickly decompose dead flesh, is surprisingly painless to living flesh, causing barely more than a tingle. You manage to defeat the creature without much damage. Its body held several coins and small gems, which you collect.");
                    party.Money += party.Depth * EncounterSelector.rng.Next(10, 20) + EncounterSelector.rng.Next(1, 5);

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
