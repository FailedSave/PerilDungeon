using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerilDungeon.Classes;

namespace PerilDungeon.Classes.Encounters
{
    public class BadAirEncounterChoice : IEncounterChoice
    {
        public string Text { get => "Oh no!"; set { } }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                return (Party party) =>
                {
                    List<string> messages = new List<string>();
                    foreach (Character c in party.PartyMembers)
                    {
                        if (c.CanAct)
                        {
                            c.AwardXP(5, party.Depth);
                            c.LoseHealth(EncounterSelector.rng.Next(0, 5) + party.Depth * 3);
                        }
                    }
                    Character target = party.GetRandomActiveCharacter();

                    target.LoseHealth(party.Depth * 3);
                    target.AwardXP(25, party.Depth);
                    if (target.HealthRatio < 0.3 + EncounterSelector.rng.NextDouble() * .3)
                    {
                        target.AddStatus("Petrified");
                        //target is petrified
                        if (target.Name == "Lorraine")
                        {
                            messages.Add("You inadvertently take a deep lungful of the noxious gas. You choke as your flesh immediately begins to change, starting with your chest. In a couple seconds, the foul gas has done its work, and you are a statue.");
                        }
                        else if (target.Name == "Johanna")
                        {
                            messages.Add("Johanna inadvertently takes a deep lungful of the noxious gas. She chokes as her flesh immediately begins to change. In a couple seconds, the foul gas has done its work, and Johanna is a statue.");
                        }
                        else
                        {
                            messages.Add("The cloud of miasma billows out over Cylenae. Before she has the chance to flutter away, she takes a lungful and immediately drops to the ground, choking. In a couple seconds more, the foul gas has done its work, and Cylenae is a tiny faerie statue on the dungeon floor.");
                        }
                    }
                    else
                    {
                        if (target.Name == "Lorraine")
                        {
                            messages.Add("You breathe too much of the noxious gas, but manage to get away coughing.");
                        }
                        else
                        {
                            messages.Add(target.Name + " breathes too much of the noxious gas, but manages to get away coughing.");
                        }
                    }

                    return messages;
                };
            }
            set { }
        }

        public bool IsAvailable { get { return true; } set { } }

        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return EncounterSelector.PickEncounter(p, typeof(ExplorationEncounter));
        }
    }
}
