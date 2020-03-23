using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class LampadesRefuseEncounterChoice : IEncounterChoice
    {
        public LampadesRefuseEncounterChoice()
        {
        }
        public string Text { get => "Refuse the Lampades"; set { } }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    Character target = party.GetRandomActiveCharacter();
                    if (target.HasStatus(Status.Polymorphed))
                    {
                        target.AwardXP(10, party.Depth);
                        target.AddStatus(Status.Petrified);
                        messages.Add($"The malicious nymphs jeer and taunt {target.YouOrNameLower}. Not satisfied with {target.YourOrHerLower} form as an animal, they decide to transform {target.YouOrHerLower} into a stone figurine of one.");
                    }
                    else
                    {
                        target.AwardXP(10, party.Depth);
                        target.MaxMana += 15;
                        if (target.IsPlayer)
                        {
                            messages.Add($"The nymphs jeer and taunt your party as you try to leave them behind. One of them taps the ground with her foot, and the walls seem to shift to cut off your retreat. Another one points at you, while several others throw small yew-tipped darts. The darts barely sting, but you can feel a strange magic on them enter your body. You expect to freeze into a stone statue, but you don't—instead, you shrink as your hands and feet become paws and your fine body hair grows into thick fur. You've become a cat! The lampades purr at you mockingly for a while, before jeering as they finally let you leave.");
                        }
                        else
                        {
                            messages.Add($"The nymphs jeer and taunt your party as you try to leave them behind. One of them taps the ground with her foot, and the walls seem to shift to cut off your retreat. Another one points at {target.Name}, while several others throw small yew-tipped darts at her. {target.Name} doesn't cry out, but she quickly begins changing form. Before you know what's happened, {target.Name} has been turned into a {(target.Species == Species.Human ? "dog" : "donkey—still with wings")}! The lampades hoot at her mockingly for a while before jeering as they let you leave.");
                        }
                        target.AddStatus(Status.Polymorphed);
                    }

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
