﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerilDungeon.Classes
{
    public class Character
    {
        private Random rng;
        public Character(string name)
        {
            rng = new Random();
            Name = name;
            Stats = new Dictionary<string, int>();
            Statuses = new HashSet<string>();
            CanAct = true;
            IsPlayer = false;
            Level = 1;
            Spells = new List<ISpell>();
            XP = 0;

        }

        public string Name { get; set; }
        public string YouOrName { get => IsPlayer ? "You" : Name; }
        public string YouOrNameLower { get => IsPlayer ? "you" : Name; }
        public string YouAreOrNameIs { get => IsPlayer ? "You are" : Name + " is"; }
        public string YouAreOrNameIsLower { get => IsPlayer ? "you are" : Name + " is"; }
        public string YourOrHerLower { get => IsPlayer ? "your" : "her"; }
        public string YouOrHerLower { get => IsPlayer ? "you" : "her"; }
        public string YouOrShe { get => IsPlayer ? "You" : "She"; }
        public string YouOrSheLower { get => IsPlayer ? "you" : "she"; }
        public string Possessive { get => IsPlayer ? "Your" : Name + "'s"; }
        public string PossessiveLower { get => IsPlayer ? "your" : Name + "'s"; }
        public string Verb(string secondPerson, string thirdPerson)
        {
            return (IsPlayer ? secondPerson : thirdPerson);
        }
        public string YouOrNameVerb(string secondPerson, string thirdPerson)
        {
            return (IsPlayer ? "You " + secondPerson : Name + " " + thirdPerson);
        }
        public string YouOrNameVerbLower(string secondPerson, string thirdPerson)
        {
            return (IsPlayer ? "you " + secondPerson : Name + " " + thirdPerson);
        }

        public Dictionary<string, int> Stats { get; set; }
        public HashSet<string> Statuses { get; set; }
        public bool CanAct { get; set; }

        public bool IsPlayer { get; set; } //controls whether the player is referred to as "you" in messages

        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Mana { get; set; }
        public int MaxMana { get; set; }
        public int Combat { get; set; }
        public int Thievery { get; set; }
        public int Level { get; set; }
        public int XP { get; set; }
        public Species NativeSpecies { get; set; }
        public Species Species { get; set; }

        public IEquipment MainItem { get; set; }
        public IEquipment BodyItem { get; set; }

        public List<ISpell> Spells { get; set; }

        public string Image
        {
            get
            {
                if (PortraitOverride == PortraitOverride.MadSculptor)
                {
                    return $"assets/{Name.ToLower()}-portrait-madsculptor.png";
                }

                StringBuilder sb = new StringBuilder("assets/" + Name.ToLower());
                if (BodyItem == null)
                {
                    sb.Append("-n");
                }

                if (Statuses.Contains("Petrified"))
                {
                    sb.Append("-stone");
                }
                sb.Append(".png");
                return sb.ToString();
            }
        }

        public PortraitOverride PortraitOverride { get; set; }

        public void AddStatus(string newStatus)
        {
            Statuses.Add(newStatus);
            updateCanAct();
        }

        public void RemoveStatus(string statusToRemove)
        {
            Statuses.Remove(statusToRemove);
            if (statusToRemove == "Petrified")
            {
                PortraitOverride = PortraitOverride.None;
            }
            updateCanAct();
        }

        public bool HasStatus (string status)
        {
            return Statuses.Contains(status);
        }

        private void updateCanAct()
        {//TODO
            CanAct = !(HasStatus("Petrified"));
        }
        
        public void Rest()
        {
            //Can't recover health if you're stone
            if (HasStatus("Petrified"))
            {
                return;
            }
            int recovery = rng.Next(1, Level + 1);
            recovery += rng.Next(1, 1 + (MaxHealth - Health)) / 20;
            if (BodyItem == null)
            {
                recovery -= 1; //chilly if you're naked
            }
            Health = Math.Min(MaxHealth, Health + recovery);

            recovery = 1 + rng.Next(1, 1 + (MaxMana - Mana)) / 10;
            Mana = Math.Min(MaxMana, Mana + recovery);
        }

        public void LoseHealth(int amount)
        {
            if (HasStatus("Petrified"))
            {
                return;
            }
            if (HasStatus("Protected"))
            {
                amount = (int)(amount * .75);
            }
            Health = Math.Max(0, Health - amount);
        }

        public void GainHealth(int amount)
        {
            if (HasStatus("Petrified"))
            {
                return;
            }
            Health = Math.Min(MaxHealth, Health + amount);
        }
        
        public void LoseMana(int amount)
        {
            if (HasStatus("Petrified"))
            {
                return;
            }
            Mana = Math.Max(0, Mana - amount);
        }

        public void GainMana(int amount)
        {
            if (HasStatus("Petrified"))
            {
                return;
            }
            Mana = Math.Min(MaxMana, Mana + amount);
        }

        public void AwardXP(int amount, int depth)
        {
            //You get only half XP per floor you are too high
            if (depth < Level)
            {
                amount = (int)(amount / (Math.Pow(2, Level - depth)));
            }

            XP += amount;
            checkLevel();
        }

        public void DestroyBodyItem()
        {
            if (BodyItem != null)
            {
                BodyItem.Unequip(this);
            }
            BodyItem = null;
        }

        private void checkLevel()
        {
            int requirement = Level * 25 + 75; //100, 125, 150...
            if (XP > requirement)
            {
                XP -= requirement;
                gainLevel();
            }
        }

        private void gainLevel()
        {
            Level += 1;
            Combat += 1;
            Thievery += 1;
            MaxHealth += 10;
            MaxMana += 10;
        }

        public double HealthRatio { get => (double)Health / (double)MaxHealth; }

        //roughly, if 2dSkill / 2 > target
        public bool CheckSkill(int skill, int target)
        {
            double roll = (rng.NextDouble() + rng.NextDouble()) * skill;
            Console.WriteLine($"Skill check: {roll} vs. {target}");
            return (roll > target);
        }
    }

    public enum Species
    {
        Human,
        Faerie,
        Animal
    }

    public enum PortraitOverride
    {
        None,
        MadSculptor
    }
}
