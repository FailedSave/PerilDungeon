using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class BashMagicalChestEncounterChoice : IEncounterChoice
    {
        public BashMagicalChestEncounterChoice(Character character)
        {
            Character = character;
            Text = $"{Character.YouOrName}";
            IsAvailable = Character.CanAct;
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
                    Character.AwardXP(15, party.Depth);

                    messages.Add($"{Character.YouOrName} {(Character.IsPlayer ? "bash" : "bashes")} the chest with a spare dagger until the wood splits open under the assault. The coins and gems inside are yours to claim.");

                    double outcome = EncounterSelector.rng.NextDouble();
                    if (outcome < .3)
                    {//explosive trap!
                        Character.AwardXP(15, party.Depth);
                        if (Character.CheckSkill(Character.Thievery, 13 + party.Depth * 2))
                        {
                            Character.LoseHealth(5);
                            messages.Add($"The chest had a small explosive trap inside, rigged to explode when the box is opened without being unlocked. Luckily {Character.YouAreOrNameIsLower} able to duck away in time.");
                        }
                        else
                        {
                            Character.LoseHealth(25 + EncounterSelector.rng.Next(party.Depth * 10));
                            messages.Add($"The chest had a small explosive trap inside, rigged to explode when the box is opened without being unlocked. {Character.YouAreOrNameIs} unable to duck away in time, and takes the brunt of the explosion.");
                        }
                    }
                    else if (outcome < .6)
                    { //petrifying gas trap
                        Character.AwardXP(15, party.Depth);
                        if (Character.HealthRatio < 0.2 + EncounterSelector.rng.NextDouble())
                        {
                            Character.AddStatus(Status.Petrified);
                            messages.Add($"The chest was full of a noxious, petrifying gas. As the boards break open, it spills out and {Character.YouOrNameLower} {(Character.IsPlayer ? "breathe" : "breathes")} it in. The chest is open, but {Character.YouAreOrNameIsLower} a stone statue, frozen opening it.");
                        }
                        else
                        {
                            Character.LoseHealth(35 + EncounterSelector.rng.Next(party.Depth * 10));
                            messages.Add($"The chest was full of a noxious, petrifying gas. {Character.YouOrName} {(Character.IsPlayer ? "gasp and choke, but are" : "gasps and chokes, but is")} able to shake off the transformation.");
                        }                        
                    }
                    //else, it's safe
                    party.Money += party.Depth * EncounterSelector.rng.Next(15, 25) + EncounterSelector.rng.Next(1, 5);
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
