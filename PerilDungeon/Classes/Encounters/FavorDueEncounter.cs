using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class FavorDueEncounter : IEncounter
    {
        public string Title { get => "Djinni Payback"; set { } }
        public string Description { get => "As you prepare to head off, the djinni pops back out of the ring. \"The time has come for you to repay me for all that power,\" she smirks. \"And the way I'd like you to do that is by using the last of that borrowed magic to transform yourself into a statue for me. Now.\""; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new FavorDueEncounterChoice());
                return choices;
            }
            set { }
        }
    }
}
