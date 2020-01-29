using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerilDungeon.Classes.Encounters;

namespace PerilDungeon.Classes
{
    public class EncounterSelector
    {
        public static Random rng;
        public static IEncounter PickEncounter(int depth)
        {
            if (rng == null)
            {
                rng = new Random();
            }
            return new BadAirEncounter();
        }
    }
}
