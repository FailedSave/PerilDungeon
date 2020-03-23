using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class InspirationEncounter : IEncounter
    {
        public string Title { get => "Inspiration!"; set { } }
        public string Description 
        {
            get
            {
                if (Party == null)
                {
                    return "Error: party missing";
                }
                else
                {
                    inspired = Party.GetActiveCharactersWithStatus(Status.Inspired)[0];
                    return $"As your party pressed on, out of nowhere, a stroke of brilliant inspiration strikes {inspired.YouOrNameLower}! This—right here, right now—would be the perfect place for one of {inspired.YourOrHerLower} companions to be a beautiful, perfectly placed statue to adorn the dungeon!";
                }
            }
            set { }
        }
        private Character inspired;
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();

                choices.Add(new InspirationAcceptEncounterChoice(inspired));
                choices.Add(new InspirationRejectEncounterChoice(inspired));

                return choices;
            }
            set { }
        }
    }
}
