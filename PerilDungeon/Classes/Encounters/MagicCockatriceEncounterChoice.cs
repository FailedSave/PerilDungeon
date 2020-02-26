using System;
using System.Collections.Generic;
using System.Text;

namespace PerilDungeon.Classes.Encounters
{
    class MagicCockatriceEncounterChoice : IEncounterChoice
    {
        public MagicCockatriceEncounterChoice(Character character)
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
                    int manaRequired = 20 + 10 * party.Depth;

                    if (Character.Mana >= manaRequired)
                    {
                        messages.Add($"Mustering all of {Character.YourOrHerLower} magical strength, {Character.YouOrNameLower} {(Character.IsPlayer ? "unleash" : "unleashes")} a deadly barrage of magical fire at the cockatrices. They drop to the ground dead, long before posing any threat to your party.");
                        Character.LoseMana(manaRequired);
                    }
                    else if (Character.Mana >= (manaRequired / 2))
                    {
                        if ((EncounterSelector.rng.NextDouble() < .6))
                        {
                            messages.Add($"Firing judiciously to conserve {Character.YourOrHerLower} magical strength, {Character.YouOrNameLower} {(Character.IsPlayer ? "unleash" : "unleashes")} precise blasts of deadly magical force at the cockatrices. One or two come close, but they're picked off before they cause any harm.");
                            Character.LoseMana(manaRequired / 2);
                        }
                        else
                        {
                            messages.Add($"Firing judiciously to conserve {Character.YourOrHerLower} magical strength, {Character.YouOrNameLower} {(Character.IsPlayer ? "unleash" : "unleashes")} precise blasts of deadly magical force at the cockatrices. Although {Character.YouAreOrNameIsLower} accurate, one manages to escape the blasts and fly straight at {Character.PossessiveLower} face. In seconds, {Character.YouAreOrNameIsLower} petrified.");
                            Character.LoseMana(manaRequired / 2);
                            Character.AddStatus(Status.Petrified);
                        }
                    }
                    else
                    {
                        if ((EncounterSelector.rng.NextDouble() < .25))
                        {
                            messages.Add($"Firing sparingly to conserve {Character.YourOrHerLower} magical strength, {Character.YouOrNameLower} {(Character.IsPlayer ? "unleash" : "unleashes")} precise blasts of deadly magical force at the cockatrices. Amazingly, they're all picked off before they cause any harm.");
                            Character.LoseMana(1000); //all of it
                        }
                        else
                        {
                            messages.Add($"Firing sparingly to conserve {Character.YourOrHerLower} magical strength, {Character.YouOrNameLower} {(Character.IsPlayer ? "unleash" : "unleashes")} precise blasts of deadly magical force at the cockatrices. It's nowhere near enough. Several fly straight at {Character.YouOrNameLower} and attack. In seconds, {Character.YouAreOrNameIsLower} petrified.");
                            Character.LoseMana(1000);
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
