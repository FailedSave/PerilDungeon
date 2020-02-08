using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public enum StairsDirection
    {
        Ascend,
        Descend,
        Ignore
    }

    public class StairsEncounterChoice : IEncounterChoice
    {
        public StairsEncounterChoice(string text, StairsDirection direction, bool enabled)
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
                    switch (Direction)
                    {
                        case StairsDirection.Ascend:
                            {
                                messages.Add("You climb up the stairs, away from the worst of the danger.");
                                party.Depth--;
                                break;
                            }
                        case StairsDirection.Descend:
                            {
                                messages.Add("You climb down the stairs, toward increasing danger and reward.");
                                party.Depth++;
                                break;
                            }
                        case StairsDirection.Ignore:
                            {
                                messages.Add("You ignore the stairs and continue on your way.");
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
