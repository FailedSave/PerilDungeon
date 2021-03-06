﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class CustomEncounterChoice : IEncounterChoice
    {
        public CustomEncounterChoice(string text, Func<Party, IEnumerable<string>> choose)
        {
            Text = text;
            Choose = choose;
        }

        public string Text { get; set; }
        public Func<Party, IEnumerable<string>> Choose { get; set; }
        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return EncounterSelector.PickEncounter(p, typeof(BasicEncounter));
        }
        public bool IsAvailable { get { return true; } set { } }
    }
}
