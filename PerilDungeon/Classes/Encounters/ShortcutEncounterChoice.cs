using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class ShortcutEncounterChoice : IEncounterChoice
    {
        public ShortcutEncounterChoice(string text, StairsDirection direction, bool enabled)
        {
            Text = text;
            Direction = direction;
            IsAvailable = enabled;
        }

        public StairsDirection Direction { get; set; }

        public string Text { get; set; }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();

                return (Party party) =>
                {
                    party.EncountersSinceStairs = 0;
                    int bestThievery = party.PartyMembers.Max(c => c.HasStatus(Status.Petrified) ? 0 : c.Thievery);
                    bool thieveryCheck = new Character("null").CheckSkill(bestThievery, 20);
                    Character target = party.GetRandomActiveCharacter();
                    switch (Direction)
                    {
                        case StairsDirection.Ascend:
                            {
                                party.Depth = Math.Max(party.Depth - 3, 1);

                                if (thieveryCheck)
                                {
                                    messages.Add("Your party manages to climb a long way up the shaft. With sharp eyes, you avoid several hidden tripwires, false footholds, and pressure triggers along the way.");
                                }
                                else
                                {
                                    if (EncounterSelector.rng.NextDouble() < .5)
                                    {
                                        messages.Add($"Your party manages to climb a long way up the shaft. However, {target.YouOrNameLower} inadvertently trips a small trigger, which sends a cascade of rocks down on {target.YourOrHerLower}. {target.YouOrShe} takes a nasty fall and ends up badly bruised, but alive.");
                                        target.LoseHealth(25 + EncounterSelector.rng.Next(10));
                                    }
                                    else
                                    {
                                        messages.Add($"Your party manages to climb a long way up the shaft without incident. However, as your party rushes out of the cramped tunnel, {target.YouOrNameLower} trips over a concealed wire, which sends a tiny dart into {target.YourOrHerLower} leg. The wound is inconsequential, but the damage is done by the poison it bears: {target.YouAreOrNameIsLower} quickly turned to stone by the concentrated basilisk venom.");
                                        target.LoseHealth(EncounterSelector.rng.Next(5));
                                        target.AddStatus(Status.Petrified);
                                    }
                                }
                                break;
                            }
                        case StairsDirection.Descend:
                            {
                                party.Depth += 3;

                                if (thieveryCheck)
                                {
                                    messages.Add("Your party manages to climb a long way down the shaft. With sharp eyes, you avoid several hidden tripwires, false footholds, and pressure triggers along the way.");
                                }
                                else
                                {
                                    if (EncounterSelector.rng.NextDouble() < .5)
                                    {
                                        messages.Add($"Your party manages to climb a long way down the shaft. However, {target.YouOrNameLower} inadvertently trips a small trigger, which sends a cascade of rocks down on {target.YourOrHerLower}. {target.YouOrShe} takes a nasty fall and ends up badly bruised, but alive.");
                                        target.LoseHealth(25 + EncounterSelector.rng.Next(10));
                                    }
                                    else
                                    {
                                        messages.Add($"Your party manages to climb a long way down the shaft without incident. However, as your party rushes out of the cramped tunnel, {target.YouOrNameLower} trips over a concealed wire, which sends a tiny dart into {target.YourOrHerLower} leg. The wound is inconsequential, but the damage is done by the poison it bears: {target.YouAreOrNameIsLower} quickly turned to stone by the concentrated basilisk venom.");
                                        target.LoseHealth(EncounterSelector.rng.Next(5));
                                        target.AddStatus(Status.Petrified);
                                    }
                                }

                                break;
                            }
                        case StairsDirection.Ignore:
                            {
                                messages.Add("This shortcut looks like more trouble than it's worth. You continue on your way.");
                                break;
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
            return EncounterSelector.PickEncounter(p, null);
        }
    }
}
