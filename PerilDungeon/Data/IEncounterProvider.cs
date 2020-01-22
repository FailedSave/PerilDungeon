using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerilDungeon.Classes;

namespace PerilDungeon.Data
{
    interface IEncounterProvider
    {
        public string EncounterName { get; set; }
        public List<EncounterChoice> Encounters { get; set; }
    }
}
