using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerilDungeon.Classes;

namespace PerilDungeon.Data
{
    interface IEncounterProvider
    {
        public IEncounter NextEncounter { get; set; }
        event Action RefreshRequested;
        void CallRequestRefresh();
    }
}
