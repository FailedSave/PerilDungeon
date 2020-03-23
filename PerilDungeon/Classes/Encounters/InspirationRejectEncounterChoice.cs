using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class InspirationRejectEncounterChoice : IEncounterChoice
    {
        public InspirationRejectEncounterChoice(Character inspired)
        {
            Inspired = inspired;
        }
        public Character Inspired { get; set; }
        public string Text { get => "Fight It"; set { } }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    Inspired.LoseMana(25);
                    Inspired.LoseHealth(10);
                    Inspired.MaxMana -= 10;
                    messages.Add($"{Inspired.YouOrNameVerb("grit your teeth and fight", "grits her teeth and fights")} off the overwhelming urges that are compelling {Inspired.YouOrHerLower} to transform someone right here and now. The experience is painful and draining, but eventually the compulsion lapses.");
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
