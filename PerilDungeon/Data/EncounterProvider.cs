using PerilDungeon.Classes;
using PerilDungeon.Classes.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Data
{
    public class EncounterProvider : IEncounterProvider
    {
        public EncounterProvider()
        {
            Encounter = new BasicEncounter();
        }

        public IEncounter Encounter { get; set; }
    }
}
