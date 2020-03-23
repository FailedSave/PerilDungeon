using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class ShortcutEncounter : IEncounter
    {
        public string Title { get => "Hidden Shortcut"; set { } }
        public string Description { get => "Passing by what seems to be a crack in the wall, you notice that it's actually a well-concealed entry to a long vertical shaft. You can use it to ascend or descend rapidly, but there might be traps that are difficult to detect. (Your character with the best Thievery will do her best to keep your group safe should you encounter one.)"; set { } }
        public Party Party { get; set; }

        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                if (Party.Depth == 1)
                {
                    choices.Add(new EscapeEncounterChoice(Party));
                }
                else
                {
                    choices.Add(new ShortcutEncounterChoice("Ascend", StairsDirection.Ascend, true));
                }
                choices.Add(new ShortcutEncounterChoice("Descend", StairsDirection.Descend, true));
                choices.Add(new ShortcutEncounterChoice("Ignore the Shortcut", StairsDirection.Ignore, true));
                return choices;
            }
            set { }
        }
    }
}
