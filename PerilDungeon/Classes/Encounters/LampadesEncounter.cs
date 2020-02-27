using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class LampadesEncounter : IEncounter
    {
        public string Title { get => "Underworld Nymphs"; set { } }
        public string Description { get => "As you explore the dungeon, a wall seems to fall away before your very eyes. In a cavern behind it, watching you, are a group of <em>lampades</em>--nymphs of the underworld and caverns, sacred to witches. They insist that you join them in their strange revelry, strongly hinting that you'll regret it if you don't."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new LampadesAcceptEncounterChoice());
                choices.Add(new LampadesRefuseEncounterChoice());
                return choices;
            }
            set { }
        }
    }
}
