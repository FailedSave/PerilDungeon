using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerilDungeon.Classes;

namespace PerilDungeon.Classes.Encounters
{
    public class BasicEncounter : IEncounter
    {
        public string Title { get => "Searching the Dungeon"; set { } }
        public string Description { get => "You can search hastily or cautiously, or rest."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                choices.Add(new CustomEncounterChoice("Rest",
                    (Party party, Character character) =>
                    {
                        List<string> messages = new List<string>();
                        messages.Add("You feel slightly refreshed.");
                        return messages;
                    }));
                choices.Add(new CustomEncounterChoice("Explore Cautiously",
                    (Party party, Character character) =>
                    {
                        List<string> messages = new List<string>();
                        messages.Add("You find a shiny coin.");
                        return messages;
                    }));
                choices.Add(new CustomEncounterChoice("Explore Recklessly",
                    (Party party, Character character) =>
                    {
                        List<string> messages = new List<string>();
                        Character target = party.GetRandomCharacter();
                        if (target.HasStatus("Petrified"))
                        {
                            return messages;
                        }
                        target.AddStatus("Petrified");
                        if (target.IsPlayer)
                        {
                            messages.Add("You are now a statue.");
                        }
                        else
                        {
                            messages.Add(target.Name + " is now a statue.");
                        }
                        return messages;
                    }));
                return choices;
            }
            set { }
        }
    }
}
