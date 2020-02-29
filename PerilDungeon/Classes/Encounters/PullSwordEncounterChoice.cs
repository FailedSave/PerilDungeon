using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class PullSwordEncounterChoice : IEncounterChoice
    {
        public PullSwordEncounterChoice(Character character)
        {
            Character = character;
            Text = $"Pull the sword ({Character.YouOrName})";
            IsAvailable = Character.CanAct && !Character.HasStatus(Status.Polymorphed);
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
                    Character.AwardXP(10, party.Depth);
                    if (EncounterSelector.rng.NextDouble() < .5)
                    {
                        messages.Add($"As {Character.YouOrNameVerbLower("clasp your", "clasps her")} hand about the hilt of the sword and pull, {Character.YouOrNameVerbLower("feel", "feels")} an ancient spirit enter {(Character.IsPlayer ? "your mind, judging your memories and your deeds" : "her mind, judging her memories and her deeds")}. {Character.YouOrNameVerb("feel", "feels")} a sense of displeasure as {Character.YouOrSheLower} {(Character.IsPlayer ? "tug" : "tugs")} to no avail; the sword stays immobile as {Character.YourOrHerLower} hand on its hilt grows cold. The ancient weapon has rendered its judgment: {Character.YouAreOrNameIsLower} to serve it as its stone guardian, not its bearer.");
                        Character.AddStatus(Status.Petrified);
                    }
                    else
                    {
                        messages.Add($"As {Character.YouOrNameVerbLower("clasp your", "clasps her")} hand about the hilt of the sword and pull, {Character.YouOrNameVerbLower("feel", "feels")} an ancient spirit enter {(Character.IsPlayer ? "your mind, judging your memories and your deeds" : "her mind, judging her memories and her deeds")}. {Character.YouOrNameVerb("feel", "feels")} a sense of warmth and acceptance as {Character.YouOrSheLower} {(Character.IsPlayer ? "tug" : "tugs")}, as the sword slides out of the rock as easily as a fork from cake. The ancient weapon has rendered its judgment: {Character.YouAreOrNameIsLower} worthy to bear it against the enemies of freedom and justice.");
                        IEquipment sword = new Items.MagicalSwordEquipment();
                        sword.Equip(Character);
                        if (Character.MainItem != null)
                        {
                            Character.MainItem.Unequip(Character);
                        }
                        Character.MainItem = sword;
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
