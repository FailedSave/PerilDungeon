using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class TrappedDoorEncounter : IEncounter
    {
        public string Title { get => "Suspicious Door"; set { } }
        public string Description { get => "Your party approaches an ornate, heavy wooden door. It's covered with interesting runes and probably has something interesting behind it. On the other hand, there's very likely a trap of some kind guarding it."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new TrappedDoorEncounterChoice(c));
                }
                choices.Add(new MessageOnlyEncounterChoice("Ignore it", "You decide to leave the door alone and move on a different way."));
                return choices;
            }
            set { }
        }
    }
}
