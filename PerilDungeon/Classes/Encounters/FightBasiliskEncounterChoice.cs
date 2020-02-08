using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class FightBasiliskEncounterChoice : IEncounterChoice
    {
        public FightBasiliskEncounterChoice(Character character)
        {
            Character = character;
            Text = $"Fight the basilisk ({Character.YouOrName})";
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
                    Character.AwardXP(35, party.Depth);
                    party.Money += party.Depth * EncounterSelector.rng.Next(10, 20) + EncounterSelector.rng.Next(1, 5);
                    if (Character.CheckSkill(Character.Combat, 9 + party.Depth * 2))
                    {
                        messages.Add($"{Character.YouAreOrNameIs} able to handily dispatch the basilisk. You take its valuable eyes and venom gland as prizes to trade.");
                    }
                    else
                    {
                        if (Character.CheckSkill(Character.Combat, 9 + party.Depth * 2)) //Another check to see if you take damage
                        {
                            Character.LoseHealth(20 + party.Depth * 5 + EncounterSelector.rng.Next(1, 5));
                            messages.Add($"The basilisk gets the better of {Character.YouOrNameLower} in the fight. {Character.YouOrName} {(Character.IsPlayer ? "manage" : "manages")} to defeat it only after suffering several bites.");
                        }
                        if (EncounterSelector.rng.NextDouble() < .66) //and a 2/3 chance of transformative consequences
                        {
                            if (Character.HealthRatio < EncounterSelector.rng.NextDouble() - 0.2)
                            {
                                Character.AddStatus("Petrified");
                                if (Character.Name == "Lorraine")
                                {
                                    messages.Add("You stab the basilisk, piercing its belly, and it hisses in pain. You dodge away to strike again, but as you do, you make eye contact with the wounded beast. You throw up your arms instinctively, but it doesn't help; the lizard's magic quickly turns you into a statue.");
                                }
                                else if (Character.Name == "Johanna")
                                {
                                    messages.Add("Johanna strikes the basilisk, slicing its side open, and it hisses in pain. She spins away and prepares another strike, but as she turns, she inadvertently makes eye contact with the wounded beast. Her martial instincts keep her from losing balance, but she can do no more than frown in dismay as the lizard's magic turns her into a statue of a brave warrior.");
                                }
                                else
                                {
                                    messages.Add("Cylenae dodges and weaves as she valiantly tries to fight, but she can't keep herself from meeting the creature's gaze. As she does so, she falls roughly to the ground, and by the time she hits, Cylenae is a tiny faerie statue on the dungeon floor.");
                                }
                            }
                            else
                            {
                                Character.LoseHealth(15);
                                messages.Add($"{Character.YouOrName} {(Character.IsPlayer ? "meet" : "meets")} the basilisk's gaze, but manages to avert {Character.YourOrHerLower} eyes before it's too late.");
                            }
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
            return EncounterSelector.PickEncounter(p, typeof(ExplorationEncounter));
        }

    }
}
