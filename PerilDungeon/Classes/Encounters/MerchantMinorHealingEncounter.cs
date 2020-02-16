using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class MerchantMinorHealingEncounter : IEncounter
    {
        public string Title { get => "Friendly Orc"; set { } }
        public string Description { get => "You encounter a small orc who seems lost in the delve. You prepare for an attack, but instead she greets you happily. She has a spare healing potion and offers to sell it to you for $50."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new MerchantBuyEncounterChoice(c, Party, 50, Effect, "You pay the orc, and she hands over a small red vial. Drinking it causes some of {1} wounds to close and restores {1} vitality."));
                }
                choices.Add(new MessageOnlyEncounterChoice("Decline", "You decline the orc's offer. She shrugs, waves, and leaves you alone."));
                return choices;
            }
            set { }
        }

        private void Effect(Character c)
        {
            c.GainHealth(25);
        }
    }
}
