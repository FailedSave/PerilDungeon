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
            NextEncounter = new FirstEncounter();
        }

        public IEncounter NextEncounter { get; set; }

        public event Action RefreshRequested;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}
