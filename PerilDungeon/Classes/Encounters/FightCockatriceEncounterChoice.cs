using System;
using System.Collections.Generic;
using System.Text;

namespace PerilDungeon.Classes.Encounters
{
    class FightCockatriceEncounterChoice : IEncounterChoice
    {
        public FightCockatriceEncounterChoice(Character character)
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
                    Character.AwardXP(30, party.Depth);

                    //Faeries are good at aerial combat
                    if (Character.Species == Species.Faerie)
                    {
                        if (EncounterSelector.rng.NextDouble() < .66 || Character.CheckSkill(Character.Combat, 2 + party.Depth))
                        {
                            messages.Add($"Flitting deftly through the air, {Character.YouOrNameLower} adeptly uses her tiny dagger to battle the ungainly monsters. Soon they all lie defeated on the ground.");
                        }
                        else
                        {
                            messages.Add($"Taking to the air, {Character.YouOrNameLower} is able to fight the cockatrices with relative ease. However, she finds herself unexpectedly flanked midair! She's not able to get out without brushing against the cockatrice's petrifying beak. The creatures are easily mopped up, but {Character.YouOrNameLower} barely manages to flutter to the ground before turning to stone.");
                            Character.LoseHealth(5 + EncounterSelector.rng.Next(1, 5));
                            Character.AddStatus(Status.Petrified);
                        }
                    }
                    else
                    {
                        if (Character.CheckSkill(Character.Combat, 11 + party.Depth * 2))
                        {
                            messages.Add($"Boldly facing the cockatrices with blade in hand, {Character.YouOrNameLower} {(Character.IsPlayer ? "become" : "becomes")} a flurry of action, deftly striking one out of the air while dodging the deadly beak of the next. Soon all of the creatures are defeated.");
                        }
                        else
                        {
                            messages.Add($"{Character.YouOrName} boldly {(Character.IsPlayer ? "face" : "faces")} the cockatrices with blade in hand. Unfortunately, there are too many, and their ungainly flight and numbers overwhelm {Character.YouOrHerLower}. Although {Character.YouOrSheLower} {(Character.IsPlayer ? "handle" : "handles")} several of them, {Character.YouAreOrNameIsLower} not able to avoid their deadly beaks. By the time the monsters are subdued, {Character.YouAreOrNameIsLower} a stone statue, blade in hand.");
                            Character.LoseHealth(5 + EncounterSelector.rng.Next(1, 5));
                            Character.AddStatus(Status.Petrified);
                        }
                    }

                    party.Money += party.Depth * EncounterSelector.rng.Next(10, 20) + EncounterSelector.rng.Next(1, 5);
                    messages.Add("You ransack the abandoned cockatrice nest for its valuable eggs, and find some other baubles on previous victims.");

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
