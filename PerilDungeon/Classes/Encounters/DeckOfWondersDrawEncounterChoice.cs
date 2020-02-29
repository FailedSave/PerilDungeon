using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class DeckOfWondersDrawEncounterChoice : IEncounterChoice
    {
        public DeckOfWondersDrawEncounterChoice()
        {
        }

        public string Text { get => "Draw a Card"; set { } }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    Outcome outcome = (Outcome)EncounterSelector.rng.Next(0, (int)Outcome.Maximum);
                    Character drawer = party.GetRandomActiveCharacter();
                    switch(outcome)
                    {
                        case Outcome.HighPriestess:
                            {
                                messages.Add($"{drawer.YouOrNameVerb("draw", "draws")} the High Priestess. A phantasmal priestess of Olanna appears before you and bathes you all in a restorative green light.");
                                foreach (Character c in party.PartyMembers)
                                {
                                    if (c.HasStatus(Status.Petrified))
                                    {
                                        c.RemoveStatus(Status.Petrified);
                                    }
                                    else if (c.HasStatus(Status.Polymorphed))
                                    {
                                        c.RemoveStatus(Status.Polymorphed);
                                    }
                                    else
                                    {
                                        c.GainHealth(35 + EncounterSelector.rng.Next(10));
                                    }
                                }
                                break;
                            }
                        case Outcome.Magician:
                            {
                                messages.Add($"{drawer.YouOrNameVerb("draw", "draws")} the Magician. A spectral graybearded wizard appears bathes you all in an invigorating blue light.");
                                foreach (Character c in party.PartyMembers)
                                {
                                    c.MaxMana += 10;
                                    c.GainMana(30);
                                }
                                break;
                            }
                        case Outcome.AceOfCoins:
                            {
                                messages.Add($"{drawer.YouOrNameVerb("draw", "draws")} the Ace of Coins. A shower of gold coins appears from seemingly nowhere and spills all over the ground.");
                                party.Money += party.Depth * EncounterSelector.rng.Next(25, 30) + EncounterSelector.rng.Next(1, 5);
                                break;
                            }
                        case Outcome.Chariot:
                            {
                                messages.Add($"{drawer.YouOrNameVerb("draw", "draws")} the Chariot. The power of the card makes your feet lighter, giving you more time to explore the dungeon before the time limit passes.");
                                party.TimeRemaining += 80;
                                break;
                            }
                        case Outcome.Devil:
                            {
                                messages.Add($"{drawer.YouOrNameVerb("draw", "draws")} the Devil. A foul demon head appears, and from its mouth spills the dreaded vile miasma that pervades the dungeon.");
                                foreach (Character c in party.PartyMembers)
                                {
                                    if (c.CanAct)
                                    {
                                        c.AwardXP(5, party.Depth);
                                        c.LoseHealth(EncounterSelector.rng.Next(0, 5) + party.Depth * 3);
                                    }
                                }
                                Character target = party.GetRandomActiveCharacter();

                                target.LoseHealth(party.Depth * 3);
                                target.AwardXP(25, party.Depth);
                                if (target.HealthRatio < 0.3 + EncounterSelector.rng.NextDouble() * .3)
                                {
                                    target.AddStatus(Status.Petrified);
                                    //target is petrified
                                    if (target.Name == "Lorraine")
                                    {
                                        messages.Add("You inadvertently take a deep lungful of the noxious gas. You choke as your flesh immediately begins to change, starting with your chest. In a couple seconds, the foul gas has done its work, and you are a statue.");
                                    }
                                    else if (target.Name == "Johanna")
                                    {
                                        messages.Add("Johanna inadvertently takes a deep lungful of the noxious gas. She chokes as her flesh immediately begins to change. In a couple seconds, the foul gas has done its work, and Johanna is a statue.");
                                    }
                                    else
                                    {
                                        messages.Add("The cloud of miasma billows out over Cylenae. Before she has the chance to flutter away, she takes a lungful and immediately drops to the ground, choking. In a couple seconds more, the foul gas has done its work, and Cylenae is a tiny faerie statue on the dungeon floor.");
                                    }
                                }
                                else
                                {
                                    if (target.Name == "Lorraine")
                                    {
                                        messages.Add("You breathe too much of the noxious gas, but manage to get away coughing.");
                                    }
                                    else
                                    {
                                        messages.Add(target.Name + " breathes too much of the noxious gas, but manages to get away coughing.");
                                    }
                                }
                                break;
                            }
                        case Outcome.Euryale:
                            {
                                Character target = party.GetRandomActiveCharacter();
                                messages.Add($"{drawer.YouOrNameVerb("draw", "draws")} Euryale. Some have said that in other worlds, this card is harmless, only reducing your resistance in the future. Here, however, a huge disembodied gorgon head appears in midair and directs its baleful gaze at {target.YouOrNameLower}. {target.YouOrNameVerb("try", "tries")} to shield your gaze, but nothing can protect {target.YouOrHerLower} from being transformed into a statue by its overwhelming energy.");
                                target.AddStatus(Status.Petrified);
                                break;
                            }
                        case Outcome.Beast:
                            {
                                List<Character> nonBeasts = party.GetActiveCharactersWithoutStatus(Status.Polymorphed);
                                if (nonBeasts.Count == 0)
                                {
                                    messages.Add($"{drawer.YouOrNameVerb("draw", "draws")} the Beast. A mighty wolf apparation appears. It nods at you all approvingly and vanishes.");
                                    foreach (Character c in party.PartyMembers)
                                    {
                                        if (c.CanAct)
                                        {
                                            c.AwardXP(30, party.Depth);
                                        }
                                    }
                                }
                                else
                                {
                                    Character target = nonBeasts[EncounterSelector.rng.Next(0, nonBeasts.Count)];
                                    messages.Add($"{drawer.YouOrNameVerb("draw", "draws")} the Beast. A mighty wolf apparation appears. It stares intently at {target.YouOrNameLower} and howls,and {target.YouOrNameVerbLower("feel", "feels")} an animal spirit well up inside {target.YouOrHerLower}. Soon {target.YouAreOrNameIs} a beast on all fours, and the wolf spirit vanishes.");
                                    target.AddStatus(Status.Polymorphed);
                                }
                                break;
                            }
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

        private enum Outcome
        {
            HighPriestess,
            Magician,
            AceOfCoins,
            Chariot,
            Devil,
            Euryale,
            Beast,
            Maximum
        }
    }
}
