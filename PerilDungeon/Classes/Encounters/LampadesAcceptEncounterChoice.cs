using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class LampadesAcceptEncounterChoice : IEncounterChoice
    {
        public LampadesAcceptEncounterChoice()
        {
        }
        public string Text { get => "Join the Revelry"; set { } }
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
                            c.MaxMana += 5 * EncounterSelector.rng.Next(1, 4);
                            c.GainMana(5 * EncounterSelector.rng.Next(2, 5));
                            c.Rest();
                        }
                        else
                        {
                            messages.Add($"The lampades decide that {c.YouAreOrNameIsLower} no fun as a statue, and use their powerful magic to turn {c.YouOrHerLower} back to flesh.");
                            c.RemoveStatus(Status.Petrified);
                        }
                        if (c.BodyItem != null && EncounterSelector.rng.NextDouble() < .33)
                        {
                            c.DestroyBodyItem();
                        }
                    }
                    party.Money += party.Depth * EncounterSelector.rng.Next(10, 20) + EncounterSelector.rng.Next(1, 5);
                    party.TimeRemaining = Math.Max(party.TimeRemaining - 50, 0);

                    messages.Add("Hesitantly at first, your party joins the lampades in their carousing. Your hesitation drains away as you taste their potent underworld wine, and you fall in with their strange, magical revelries...");
                    messages.Add("You all wake up considerably later—too much time has passed! And some of your clothes are nowhere to be found! On the bright side, you've all learned a few new magical tricks, and somehow you seemed to have earned a few coins from...tips?");
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
