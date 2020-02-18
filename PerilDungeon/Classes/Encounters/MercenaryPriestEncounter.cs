using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class MercenaryPriestEncounter : IEncounter
    {
        public string Title { get => "Mercenary Priest"; set { } }
        public string Description { get => "In the darkest depths, you find a wandering, half-mad priest of Kylan himself. You listen to him preach for a little while; he tells you that there is a book, the Kylanian Apocrypha, that details a special prayer that allows the worshiper to pick up a Sunstone shard safely. However, he demands $2500 for his copy of the book."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new PriestBuyEncounterChoice(Party));
                choices.Add(new PriestAttackEncounterChoice());
                choices.Add(new MessageOnlyEncounterChoice("Leave", "You shake your head and decline the priest's offer. \"You'll be back... if you survive!\" he calls out after you."));
                return choices;
            }
            set { }
        }
    }
}
