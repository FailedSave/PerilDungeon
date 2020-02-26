using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class BetrayalEncounterChoice : IEncounterChoice
    {
        public BetrayalEncounterChoice(Character character)
        {
            Character = character;
            Text = $"Give in ({Character.YouOrName})";
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
                    int money = 0;
                    int XP = 0;
                    int bonusMana = 0;
                    foreach (Character c in party.PartyMembers)
                    {
                        if (c.Name == Character.Name)
                        {
                            continue;
                        }
                        if (!c.CanAct)
                        {
                            continue;
                        }
                        c.AddStatus(Status.Petrified);
                        money += party.Depth * EncounterSelector.rng.Next(20, 30) + EncounterSelector.rng.Next(1, 5);
                        XP += 30;
                        bonusMana += 5;
                    }

                    if (money == 0)
                    {
                        messages.Add($"Jushalan guffaws aloud at {Character.PossessiveLower} misfortune: {(Character.IsPlayer ? "you have" : "she has")} no friends left to betray. He leaves a single silver coin and a slip of paper with a line of mocking scripture.");
                        XP = 5;
                        money = 1;
                    }
                    else
                    {
                        if (Character.IsPlayer)
                        {
                            messages.Add($"With a silent prayer in your mind, you accept Jushalan's offer. As you do so, you hear aloud an uproarious, maniacal laughter. Thunder crashes and your friends try to duck for cover, but there is no hiding from the god's power: they are petrified where they stand. Meanwhile, you feel a burst of vigor and knowledge, and a large clear diamond appears on the ground.");
                        }
                        else
                        {
                            messages.Add($"{Character.Name} gets a wicked gleam in her eye, and suddenly a malevolent laughter fills the air. You look around worriedly, and feel an oppressive divine power closing in. Suddenly, thunder crashes and you friend are reduced to a pair of statues. {Character.Name} grins in glee as she counts the rewards of her betrayal: a beautiful diamond and newfound power.");
                        }
                    }

                    party.Money += money;
                    Character.AwardXP(XP, party.Depth);
                    Character.MaxMana += bonusMana;
                    Character.GainMana(bonusMana * 3);
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
