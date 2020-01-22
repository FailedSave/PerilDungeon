using PerilDungeon.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Data
{
    public class EncounterProvider : IEncounterProvider
    {
        public string EncounterName { get { return "Searching the Dungeon"; } set { } }
        public List<EncounterChoice> Encounters {
            get
            {
                List<EncounterChoice> encounters = new List<EncounterChoice>();
                encounters.Add(new EncounterChoice("Search Cautiously"));
                encounters.Add(new EncounterChoice("Proceed Recklessly"));
                return encounters;
            }
             set => throw new NotImplementedException();
        }
    }
}
