using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class CharacterCastSpellEncounterChoice : IEncounterChoice
    {
        public CharacterCastSpellEncounterChoice(Character character)
        {
            Character = character;
            Text = $"{Character.YouOrName}";
            IsAvailable = Character.CanAct && Character.Mana >= 100;
        }
        public Character Character;

        public string Text { get; set; }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    Character target = null;
                    foreach (Character possibleTarget in party.PartyMembers)
                    {
                        if (possibleTarget.HasStatus(Status.Petrified) || possibleTarget.HasStatus(Status.Polymorphed))
                        {
                            target = possibleTarget;
                            break;
                        }
                    }
                    if (target != null)
                    {
                        messages.AddRange(Character.Spells[0].Cast(Character, target));
                    }
                    else
                    {
                        messages.Add("Nobody needs to be restored right now.");
                    }
                    return messages;
                };
            }
            set { }
        }
        public bool IsAvailable { get; set; }

        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return EncounterSelector.PickEncounter(p, typeof(ExplorationEncounter));
        }

    }
}
