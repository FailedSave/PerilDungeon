using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class InspirationAcceptEncounterChoice : IEncounterChoice
    {
        public InspirationAcceptEncounterChoice(Character inspired)
        {
            Inspired = inspired;
        }
        public Character Inspired { get; set; }
        public string Text { get => "Be Inspired"; set { } }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    List<Character> possibleTargets = party.GetActiveCharactersWithoutStatus(Status.Petrified);
                    if (possibleTargets.Count == 1)
                    {
                        messages.Add($"This is the time. This is the place for a beautiful tableau of the three of you to stand eternally. {Inspired.YouOrNameVerb("focus your", "focuses her")} transformative magic on {Inspired.YourOrHerLower}self. The spell is cast, and in moments your party is just as was destined—a trio of beautiful statues for the dungeon.");
                        Inspired.LoseMana(50);
                        possibleTargets[0].AddStatus(Status.Petrified);
                    }
                    else
                    {
                        Character target = party.GetRandomActiveCharacter();
                        while (target == Inspired)
                        {
                            target = party.GetRandomActiveCharacter();
                        }
                        messages.Add($"This is the time. This is the place. {Inspired.YouOrNameVerb("concentrate", "concentrates")} on the lessons of the Tome of Transfiguration, and {(Inspired.IsPlayer ? "reaslize" : "realizes")} that {target.YouOrNameLower} would make a perfect statue to decorate the Delve right here. {Inspired.YouOrNameVerb("get", "gets")} a wicked gleam in {Inspired.YourOrHerLower} eye, and in the blink of an eye, {target.YouAreOrNameIsLower} a beautiful statue. The experience further increases {Inspired.PossessiveLower} knowledge of magic.");
                        target.AddStatus(Status.Petrified);
                        Inspired.LoseMana(50);
                        Inspired.MaxMana += 10;
                        if (EncounterSelector.rng.NextDouble() < .4)
                        {
                            messages.Add($"{Inspired.YouOrNameVerb("feel", "feels")} as if there are more secrets to discover from the Tome...");
                        }
                        else 
                        {
                            messages.Add($"{Inspired.YouOrNameVerb("finally feel", "finally feels")} as if {Inspired.YouOrSheLower} has learned all that can be learned from the Tome.");
                            Inspired.RemoveStatus(Status.Inspired);
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
    }
}
