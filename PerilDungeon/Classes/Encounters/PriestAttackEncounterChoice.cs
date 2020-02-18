using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class PriestAttackEncounterChoice : IEncounterChoice
    {
        public PriestAttackEncounterChoice()
        {
            Text = $"Attack the priest";
            IsAvailable = true;
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
                        c.AwardXP(50, party.Depth); //at least you're brave
                    }
                    int totalCombat = party.PartyMembers.Sum(c => c.CanAct ? c.Combat : 0);
                    if (party.PartyMembers[0].CheckSkill(totalCombat, 45))
                    {
                        foreach (Character c in party.PartyMembers)
                        {
                            c.LoseHealth(40 + EncounterSelector.rng.Next(10));
                        }
                        messages.Add("Working together, your party attacks the priest. He fights ferociously with divine power, but with your combat experience and superior numbers, you manage to get the best of him. As he dies, he moans the words \"I... was... unworthy...\"");
                        messages.Add("You claim the small, worn book. Written by a centuries-old saint, it descibes specific prayers that must be recited while claiming a Sunstone shard to pick it up without incurring Kylan's wrath. You keep it with you so you'll be able to remember it when the time comes.");

                        party.MainQuestProgress = MainQuestProgress.GotBook;
                    }
                    else
                    {
                        Character target = party.GetRandomActiveCharacter();
                        if (EncounterSelector.rng.Next() < .5)
                        {
                            target.LoseHealth(10000);
                            messages.Add($"Your party prepares to attack the priest, but with a dismissive wave of his hand, he summons a mighty fist of force that strikes {target.YouOrNameLower} squarely, sending {target.YouOrHerLower} sprawling across the room unconscious. He smirks as he leaves, having demonstrated he is more than a match for you.");
                        }
                        else
                        {
                            target.AddStatus("Petrified");
                            messages.Add($"Your party prepares to attack the priest, but with a dismissive wave of his hand, he utters a short prayer to Kylan, and in an instant {target.YouAreOrNameIsLower} transformed into stone. Your planned attack broken, he smirks as he leaves, having demonstrated he is more than a match for you.");
                        }
                    }

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
