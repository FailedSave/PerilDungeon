using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class MerchantMajorHealingEncounter : IEncounter
    {
        public string Title { get => "Friendly Orc Captain"; set { } }
        public string Description { get => "You encounter a big orc, a chieftain among her kind. She seems hale and hearty and not in any particular need of you help. She offers you a large healing potion for $250."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new MerchantBuyEncounterChoice(c, Party, 250, Effect, "You pay the orc, and she hands over a big red vial. Drinking it causes all of {1} wounds to close and makes {0} feel stronger than ever before!"));
                }
                choices.Add(new MessageOnlyEncounterChoice("Decline", "You decline the orc's offer. She shrugs, waves, and leaves you alone."));
                return choices;
            }
            set { }
        }

        private void Effect(Character c)
        {
            c.MaxHealth += 10;
            c.GainHealth(10000);
        }
    }
}
