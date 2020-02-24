using System;
using System.Collections.Generic;
using System.Text;

namespace PerilDungeon.Classes.Encounters
{
    class CockatriceEncounter : IEncounter
    {
        public string Title { get => "Flock of Cockatrices"; set { } }
        public string Description { get => "You hear the ungainly flapping of many clumsy wings flying down the hall. As they draw close, you recognize a small flock of misshapen birds: cockatrices! A single one of these chicken-sized monsters would be little threat for your team, but this small flock means one is likely to strike you with its petrifying touch if you try to fight."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new FollowUpEncounterChoice("Fight them", "One of you steps forward to fight them while the others give her space to fight freely...", typeof(FightCockatriceEncounter)));
                choices.Add(new FollowUpEncounterChoice("Use magic", "One of you prepares a barrage of magic darts to bring down the flock...", typeof(MagicCockatriceEncounter)));
                choices.Add(new FleeCockatriceEncounterChoice());
                return choices;
            }
            set { }
        }
    }
}
